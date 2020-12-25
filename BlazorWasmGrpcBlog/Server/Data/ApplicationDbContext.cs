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
		}
	}
}
