# Blazor Wasm gRPC Blog Sample

Tip: Watch: On .NET Deep Dive into Many-to-Many: [A Tour of EF Core 5.0 pt. 2](https://channel9.msdn.com/Shows/On-NET/Deep-Dive-into-Many-to-Many-A-Tour-of-EF-Core-50-pt-2)

**Working** (CTRL-F5) Kestrel Hosted Blazor 5.x WASM Sample Project (with Identity) for gRPC with related (EF Core/SQLite) data. 

Many to many `.Include` results in a stack overflow, is this a reference loop?

(part of) /Server/Services/[BlogService.cs](https://github.com/JeepNL/BlazorWasmGrpcBlog/blob/master/BlazorWasmGrpcBlog/Server/Services/BlogService.cs)
 
			var posts = new Posts();
			var allPosts = await dbContext.Posts
				.Where(ps => ps.PostStat == PostStatus.Published)
				.Include(pa => pa.PostAuthor)
				.Include(pe => pe.PostExt)
				//.Include(tipd => tipd.TagsInPostData) // TODO: [ERROR] / doesn't work: results in a stack overflow.
				.OrderByDescending(dc => dc.DateCreated)
				.ToListAsync();
			posts.PostsData.AddRange(allPosts);
			return posts;

Quick Links to important files
 - /Shared/Protos/[blog.proto](https://github.com/JeepNL/BlazorWasmGrpcBlog/blob/master/BlazorWasmGrpcBlog/Shared/Protos/blog.proto)
 - /Server/Data/[ApplicationDbContext.cs](https://github.com/JeepNL/BlazorWasmGrpcBlog/blob/master/BlazorWasmGrpcBlog/Server/Data/ApplicationDbContext.cs)
 - /Server/Data/[SeedData.cs](https://github.com/JeepNL/BlazorWasmGrpcBlog/blob/master/BlazorWasmGrpcBlog/Server/Data/SeedData.cs)
 - /Server/Services/[BlogService.cs](https://github.com/JeepNL/BlazorWasmGrpcBlog/blob/master/BlazorWasmGrpcBlog/Server/Services/BlogService.cs)

Adding data to join table solved, see: https://github.com/dotnet/efcore/issues/23703#issuecomment-758801618

/Server/Data/ApplicationDbContext.cs

    modelBuilder.Entity<Post>().Navigation(e => e.TagsInPostData).HasField("tagsInPostData_");
    modelBuilder.Entity<Tag>().Navigation(e => e.PostsInTagData).HasField("postsInTagData_");
 
