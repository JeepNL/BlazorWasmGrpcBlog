using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using BlazorWasmGrpcBlog.Server.Models;
using BlazorWasmGrpcBlog.Shared;
using BlazorWasmGrpcBlog.Shared.Protos;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmGrpcBlog.Server.Data
{
	public class SeedData
	{
		private readonly ApplicationDbContext ctx;
		public SeedData(ApplicationDbContext dbContext)
		{
			ctx = dbContext;
		}

		public void BlogSeed()
		{
			// ctx.Database.EnsureDeleted(); // without migrations
			// ctx.Database.EnsureCreated(); // without migrations
			ctx.Database.Migrate(); // with migrations

			if (ctx.Posts.Any())
			{
				return;
			}

			var utcDate = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
			var utcTs = DateTimeOffset.UtcNow.ToTimestamp();

			// Seed Authors
			ctx.Authors.Add(new Author()
			{
				Id = 1,
				Name = "Author I",
				DateCreated = utcDate,
			});
			ctx.Authors.Add(new Author()
			{
				Id = 2,
				Name = "Author II",
				DateCreated = utcDate
			});
			ctx.Authors.Add(new Author()
			{
				Id = 3,
				Name = "Author III",
				DateCreated = utcDate
			});

			// Seed Tags
			ctx.Tags.Add(new Tag()
			{
				Id = "TagOne"
			});

			ctx.Tags.Add(new Tag()
			{
				Id = "TagTwo"
			});

			ctx.Tags.Add(new Tag()
			{
				Id = "TagThree"
			});

			// Seed Posts
			ctx.Posts.Add(new Post()
			{
				Id = 1,
				AuthorId = 1,
				Title = "First Post",
				DateCreated = utcDate,
				PostStat = PostStatus.Published,
			});
			ctx.Posts.Add(new Post()
			{
				Id = 2,
				AuthorId = 2,
				Title = "Second Post",
				DateCreated = utcDate,
				PostStat = PostStatus.Published
			});
			ctx.Posts.Add(new Post()
			{
				Id = 3,
				AuthorId = 3,
				Title = "Third Post",
				DateCreated = utcDate,
				PostStat = PostStatus.Published
			});

			// Seed Extended info to Posts
			ctx.PostsExtented.Add(new PostExtended()
			{
				PostId = 1,
				Ts = utcTs
			}); ;
			ctx.PostsExtented.Add(new PostExtended()
			{
				PostId = 2,
				Ts = utcTs
			});
			ctx.PostsExtented.Add(new PostExtended()
			{
				PostId = 3,
				Ts = utcTs
			});

			ctx.SaveChanges();
		}
	}
}
