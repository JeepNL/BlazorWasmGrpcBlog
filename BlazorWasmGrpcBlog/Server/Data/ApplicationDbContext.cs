﻿using BlazorWasmGrpcBlog.Server.Models;
using BlazorWasmGrpcBlog.Shared.Protos;
using IdentityServer4.EntityFramework.Entities;
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
		public DbSet<PostExtended> PostsExtented { get; set; }
		public DbSet<Tag> Tags { get; set; }

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var tsConverter = new ValueConverter<Google.Protobuf.WellKnownTypes.Timestamp, string>(
				 v => v == null ? null : v.ToDateTimeOffset().ToString("yyyy-MM-dd HH:mm:ss zzz"), // To DB
				 v => v == null ? null : Google.Protobuf.WellKnownTypes.Timestamp
					.FromDateTimeOffset(DateTime.ParseExact(v, "yyyy-MM-dd HH:mm:ss zzz", System.Globalization.CultureInfo.InvariantCulture)) // From DB
			);
			modelBuilder.Entity<PostExtended>().Property(e => e.Ts).HasConversion(tsConverter);

			modelBuilder.Entity<PostExtended>()
				.HasKey(c => c.PostId);

			modelBuilder.Entity<PostExtended>()
				.Property(c => c.PostId)
				.ValueGeneratedNever();
		}
	}
}
