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
				.Include(p => p.Author)
				.Include(q => q.PostExtended)
				.ToListAsync();
			posts.PostsData.AddRange(allPosts);
			return posts;
		}

		public override async Task<Post> GetPost(GetPostQuery request, ServerCallContext context)
		{
			return await dbContext.Posts
				.Include(p => p.Author)
				.Include(q => q.PostExtended)
				.SingleOrDefaultAsync(post => post.Id == request.Id);
		}

		public override async Task<Posts> GetAuthorPosts(GetAuthorPostsQuery request, ServerCallContext context)
		{
			var authorPosts = new Posts();
			var allPosts = await dbContext.Posts
				.Where(p => p.AuthorId == request.Id)
				.ToListAsync();
			authorPosts.PostsData.AddRange(allPosts);
			return authorPosts;
		}


		public override async Task<Authors> GetAuthors(Empty request, ServerCallContext context)
		{
			var authors = new Authors();
			var allAuthors = await dbContext.Authors
				.ToListAsync();
			authors.AuthorsData.AddRange(allAuthors);
			return authors;
		}

	}
}
