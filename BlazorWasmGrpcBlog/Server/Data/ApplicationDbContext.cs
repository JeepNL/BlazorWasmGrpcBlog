using BlazorWasmGrpcBlog.Server.Models;
using BlazorWasmGrpcBlog.Shared.Protos;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmGrpcBlog.Server.Data
{
	// Info
	// SQLite DB
	// Package Manager Console:
	// Clear; Add-Migration InitialCreate -OutputDir "Data/Migrations"; Update-Database;

	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public ApplicationDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}
		public DbSet<Author> Authors { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var tsConverter = new ValueConverter<Google.Protobuf.WellKnownTypes.Timestamp, string>(
				v => v.ToDateTimeOffset().ToString("yyyy-MM-dd HH:mm:ss"),
				v => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(DateTimeOffset.UtcNow)
			);

			modelBuilder.Entity<Post>().Property(e => e.Date).HasConversion(tsConverter);

			// seed Authors
			modelBuilder
				.Entity<Author>()
				.HasData(
					new Author { AuthorId = 1, Name = "First Author" },
					new Author { AuthorId = 2, Name = "Second Author" },
					new Author { AuthorId = 3, Name = "Third Author" }
				);

			// seed Posts
			modelBuilder
				.Entity<Post>()
				.HasData(
					new Post { PostId = 1, AuthorId = 1, Title = "First Post" },
					new Post { PostId = 2, AuthorId = 2, Title = "Second Post" },
					new Post { PostId = 3, AuthorId = 3, Title = "Third Post" }
				);

			// seed Tags #TODO
			modelBuilder
				.Entity<Tag>()
				.HasData(
					new Tag { TagId = "Tag1" },
					new Tag { TagId = "Tag2" },
					new Tag { TagId = "Tag3" }
				);
		}
	}
}
