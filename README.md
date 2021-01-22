# Blazor Wasm gRPC Blog Sample

 - **Working** (CTRL-F5) Kestrel Hosted Blazor 5.x WASM Sample Project (with Identity) for gRPC with related (EF Core/SQLite) data. 

Many-to-Many working now, see: https://github.com/grpc/grpc-dotnet/issues/1177#issuecomment-763910215

Adding data to join table solved, see: https://github.com/dotnet/efcore/issues/23703#issuecomment-758801618

(part of) /Server/Data/[ApplicationDbContext.cs](https://github.com/JeepNL/BlazorWasmGrpcBlog/blob/master/BlazorWasmGrpcBlog/Server/Data/ApplicationDbContext.cs)

    modelBuilder.Entity<Post>().Navigation(e => e.TagsInPostData).HasField("tagsInPostData_");
    modelBuilder.Entity<Tag>().Navigation(e => e.PostsInTagData).HasField("postsInTagData_");

Tip: Watch: On .NET Deep Dive into Many-to-Many: [A Tour of EF Core 5.0 pt. 2](https://channel9.msdn.com/Shows/On-NET/Deep-Dive-into-Many-to-Many-A-Tour-of-EF-Core-50-pt-2)

----

Quick Links to important files
 - /Shared/Protos/[blog.proto](https://github.com/JeepNL/BlazorWasmGrpcBlog/blob/master/BlazorWasmGrpcBlog/Shared/Protos/blog.proto)
 - /Server/Data/[ApplicationDbContext.cs](https://github.com/JeepNL/BlazorWasmGrpcBlog/blob/master/BlazorWasmGrpcBlog/Server/Data/ApplicationDbContext.cs)
 - /Server/Data/[SeedData.cs](https://github.com/JeepNL/BlazorWasmGrpcBlog/blob/master/BlazorWasmGrpcBlog/Server/Data/SeedData.cs)
 - /Server/Services/[BlogService.cs](https://github.com/JeepNL/BlazorWasmGrpcBlog/blob/master/BlazorWasmGrpcBlog/Server/Services/BlogService.cs)
