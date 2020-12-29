using BlazorWasmGrpcBlog.Server.Data;
using BlazorWasmGrpcBlog.Shared.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
			var posts = new Posts();
			var allPosts = await dbContext.Posts
				.Where(o => o.PostStat == PostStatus.Published)
				.Include(p => p.PostAuthor)
				.Include(q => q.PostExt) // Temporary for TimeStamp
				.OrderByDescending(r => r.DateCreated)
				.ToListAsync();
			posts.PostsData.AddRange(allPosts);
			return posts;
		}

		public override async Task<Post> GetPost(GetPostQuery request, ServerCallContext context)
		{
			return await dbContext.Posts
				.Include(p => p.PostAuthor)
				.Include(q => q.PostExt)
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
