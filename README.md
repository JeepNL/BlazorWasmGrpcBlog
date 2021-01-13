# Blazor Wasm gRPC Blog Sample

### 🛠 Under Construction

**Working** (CTRL-F5) Kestrel Hosted Blazor 5.x WASM Sample Project (with Identity) for gRPC with related (EF Core/SQLite) data. 
 - One to One
 - One to Many
 - Many to Many
 
Tip: Watch: On .NET Deep Dive into Many-to-Many: [A Tour of EF Core 5.0 pt. 2](https://channel9.msdn.com/Shows/On-NET/Deep-Dive-into-Many-to-Many-A-Tour-of-EF-Core-50-pt-2)
 
Adding data to Many to Many Join Table 'PostsTags' now works because of just the 2 lines below.

See: https://github.com/dotnet/efcore/issues/23703#issuecomment-758801618

/Server/Data/ApplicationDbContext.cs

    modelBuilder.Entity<Post>().Navigation(e => e.TagsInPostData).HasField("tagsInPostData_");
    modelBuilder.Entity<Tag>().Navigation(e => e.PostsInTagData).HasField("postsInTagData_");
 
