using BlazorWasmGrpcBlog.Server.Data;
using BlazorWasmGrpcBlog.Shared.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmGrpcBlog.Server.Services
{
	public class BlogService : BlogProto.BlogProtoBase
	{
		private readonly ApplicationDbContext dbContext;
		public BlogService(ApplicationDbContext dataContext)
		{
			dbContext = dataContext;
		}

		public override async Task<Posts> GetPosts(Empty request, ServerCallContext context)
		{
			var postsQuery = await dbContext.Posts.AsSplitQuery() // trying/testing ".AsSplitQuery()"
																  //var postsQuery = await dbContext.Posts
				.Where(ps => ps.PostStat == PostStatus.Published)
				.Include(pa => pa.PostAuthor)
				.Include(pe => pe.PostExtended)
				.Include(tipd => tipd.TagsInPostData)
				.OrderByDescending(dc => dc.DateCreated)
				.AsNoTracking().ToListAsync();

			// The Protobuf serializer doesn't support reference loops
			// see: https://github.com/grpc/grpc-dotnet/issues/1177#issuecomment-763910215
			//var posts = new Posts();
			//posts.PostsData.AddRange(allPosts); // so this doesn't work
			//return posts

			Posts posts = new();
			foreach (var p in postsQuery)
			{
				Post post = new()
				{
					PostId = p.PostId,
					Title = p.Title,
					DateCreated = p.DateCreated,
					PostStat = p.PostStat,
					PostAuthor = p.PostAuthor,
					PostExtended = p.PostExtended,
				};

				// Just add all the tags to each post, this isn't a reference loop.
				List<Tag> tags = p.TagsInPostData.Select(t => new Tag { TagId = t.TagId }).ToList();
				post.TagsInPostData.AddRange(tags);

				// Add Post (now with tags) to posts
				posts.PostsData.Add(post);

				// OLD: foreach loop for adding tags
				//foreach (var t in p.TagsInPostData)
				//{
				//	var tag = new Tag() { TagId = t.TagId };
				//	post.TagsInPostData.Add(tag);
				//}

			}

			// For debugging
			//foreach (var p2 in posts.PostsData)
			//{
			//	Console.Write($"PostId: {p2.PostId}, Title: {p2.Title}, Author Id: {p2.PostAuthor.AuthorId}");
			//	foreach (var t in p2.TagsInPostData)
			//	{
			//		Console.Write($", [{t.TagId}]");
			//	}
			//	Console.Write(Environment.NewLine);
			//}

			return posts;

			//
			// Ignore code below, just for testing.
			//

			//using (var conn = new SqliteConnection("Data Source=Data/BlogDB.sqlite3"))
			//{
			//	conn.Open();

			//	var command = conn.CreateCommand();
			//	command.CommandText = @"SELECT p.PostId, p.Title, pt.TagId FROM Posts p INNER JOIN PostsTags pt ON p.PostId = pt.PostId";
			//	//command.CommandText = @"SELECT PostId, Title FROM Posts";

			//	using (var reader = command.ExecuteReader())
			//	{
			//		while (reader.Read())
			//		{
			//			Console.WriteLine($"Id: {reader.GetInt32(0)}, Title: {reader.GetString(1)}, TagId: {reader.GetString(2)}");
			//			//Console.WriteLine($" Id: {reader.GetInt32(0)}, Title: {reader.GetString(1)}");
			//		}
			//	}
			//}
		}

		public override async Task<Post> GetPost(GetPostQuery request, ServerCallContext context)
		{
			return await dbContext.Posts
				.Include(pa => pa.PostAuthor)
				.Include(pe => pe.PostExtended)
				.SingleOrDefaultAsync(post => post.PostId == request.Id);
		}

		public override async Task<Authors> GetAuthors(Empty request, ServerCallContext context)
		{
			var authors = new Authors();
			var allAuthors = await dbContext.Authors
				.ToListAsync();
			authors.AuthorsData.AddRange(allAuthors);
			return authors;
		}

		public override async Task<Author> GetAuthor(GetAuthorQuery request, ServerCallContext context)
		{
			return await dbContext.Authors
				.SingleOrDefaultAsync(author => author.AuthorId == request.Id);
		}
	}
}
