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
using BlazorWasmGrpcBlog.Shared.Helpers;

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
			// 1) Delete "BlogDB.sqlite3" & "Migrations" Folder
			// 2) Run in Package Manager Console:
			//		Clear; Add-Migration InitialCreate -OutputDir "Data/Migrations"; Update-Database;

			// Extra Info
			ctx.Database.EnsureDeleted();
			ctx.Database.Migrate(); // with migrations //ctx.Database.EnsureCreated(); // without migrations

			if (ctx.Posts.Any())
			{
				return;
			}

			var utcDate = DateTimeOffset.UtcNow.ToString(DtFormats.DbUtc);
			var utcTs = DateTimeOffset.UtcNow.ToTimestamp();

			var author1 = new Author() { AuthorId = 1, Name = "Author I", DateCreated = utcDate };
			var author2 = new Author() { AuthorId = 2, Name = "Author II", DateCreated = utcDate };
			var author3 = new Author() { AuthorId = 3, Name = "Author III", DateCreated = utcDate };

			var tag1 = new Tag() { TagId = 1, Name = "TagOne" };
			var tag2 = new Tag() { TagId = 2, Name = "TagTwo" };
			var tag3 = new Tag() { TagId = 3, Name = "Tagthree" };

			var post1 = new Post() { PostId = 1, AuthorId = 1, Title = "First Post", DateCreated = utcDate, PostStat = PostStatus.Published };
			var post2 = new Post() { PostId = 2, AuthorId = 2, Title = "Second Post", DateCreated = utcDate, PostStat = PostStatus.Published };
			var post3 = new Post() { PostId = 3, AuthorId = 3, Title = "Third Post", DateCreated = utcDate, PostStat = PostStatus.Published };

			var postExtended1 = new PostExtended() { PostId = 1, Content = "Post One Content", Ts = utcTs };
			var postExtended2 = new PostExtended() { PostId = 2, Content = "Post Two Content", Ts = utcTs };
			var postExtended3 = new PostExtended() { PostId = 3, Content = "Post Three Content", Ts = utcTs };

			// Adding posts to a tag results in an error:
			//   crit: Microsoft.AspNetCore.Hosting.Diagnostics[6]
			//   Application startup exception
			//   System.InvalidOperationException: No backing field could be found for property 'Tag.PostsInTagsData' and the property does not have a setter.

			// Uncomment the 3 lines below to see the error.
			//tag1.PostsInTagsData.AddRange(new[] { post1 });
			//tag2.PostsInTagsData.AddRange(new[] { post1, post3 });
			//tag3.PostsInTagsData.AddRange(new[] { post2, post3 });

			ctx.AddRange(author1, author2, author3, tag1, tag2, tag3, post1, post2, post3, postExtended1, postExtended2, postExtended3);
			ctx.SaveChanges();
		}
	}
}
