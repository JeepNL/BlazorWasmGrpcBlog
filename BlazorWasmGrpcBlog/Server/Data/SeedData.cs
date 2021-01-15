using BlazorWasmGrpcBlog.Server.Models;
using BlazorWasmGrpcBlog.Shared.Helpers;
using BlazorWasmGrpcBlog.Shared.Protos;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
			// Info
			// SQLite DB
			// When Proto/ Model Change
			// Visual Studio:
			//		Delete /Server/Data/Migrations folder
			//		Delete /Server/Data/BlogDB.sqlite3
			// Package Manager Console:
			//		Clear; Add-Migration InitialCreate -OutputDir "Data/Migrations"; Update-Database;

			// Extra for testing only, start with a clean DB.
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

			// Extra for testing/Learning
			// See (gavilanch3) https://www.youtube.com/watch?v=yJAf5fKpGO
			//var post1Tags = new PostsTags2() { TagId = tag1.TagId, PostId = post1.PostId };
			//ctx.AddRange(author1, author2, author3, tag1, tag2, tag3, post1, post2, post3, postExtended1, postExtended2, postExtended3, post1Tags);

			// From ON.NET Deep Dive in Many to Many Part 2 https://channel9.msdn.com/Shows/On-NET/Deep-Dive-into-Many-to-Many-A-Tour-of-EF-Core-50-pt-2
			// This now works!! See: https://github.com/dotnet/efcore/issues/23703#issuecomment-758801618, change needed in ApplicationDbContext.cs
			tag1.PostsInTagData.AddRange(new[] { post1 });
			tag2.PostsInTagData.AddRange(new[] { post1, post3 });
			tag3.PostsInTagData.AddRange(new[] { post2, post3 });
			ctx.AddRange(author1, author2, author3, tag1, tag2, tag3, post1, post2, post3, postExtended1, postExtended2, postExtended3);

			ctx.SaveChanges();
		}
	}
}
