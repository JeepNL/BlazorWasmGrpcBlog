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
			if (ctx.Posts.Any())
			{
				return;
			}

			// Seed Authors
			ctx.Authors.Add(new Author()
			{
				AuthorId = 1,
				Name = "Author I"

			});
			ctx.Authors.Add(new Author()
			{
				AuthorId = 2,
				Name = "Author II"

			});
			ctx.Authors.Add(new Author()
			{
				AuthorId = 3,
				Name = "Author III"

			});

			// Seed Tags
			ctx.Tags.Add(new Tag()
			{
				TagId = "TagOne"
			});

			ctx.Tags.Add(new Tag()
			{
				TagId = "TagTwo"
			});

			ctx.Tags.Add(new Tag()
			{
				TagId = "TagThree"
			});

			// Seed Posts
			ctx.Posts.Add(new Post()
			{
				PostId = 1,
				AuthorId = 1,
				// Author = ctx.Authors.FirstOrDefault(p => p.AuthorId == 1),
				Title = "First Post",
				BlogStatus = BlogStatus.Published
			});
			ctx.Posts.Add(new Post()
			{
				PostId = 2,
				AuthorId = 2,
				Title = "Second Post",
				BlogStatus = BlogStatus.Published
			});
			ctx.Posts.Add(new Post()
			{
				PostId = 3,
				AuthorId = 3,
				Title = "Third Post",
				BlogStatus = BlogStatus.Published
			});

			ctx.SaveChanges();
		}
	}
}
