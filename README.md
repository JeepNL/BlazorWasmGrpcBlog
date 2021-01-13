# Blazor Wasm gRPC Blog Sample

### 🛠 Under Construction

Blazor 5.x WASM Sample Project for gRPC with related (EF Core/SQLite) data. 
 - One to One
 - One to Many
 - Many to Many
 
Adding data to Many to Many Join Table 'PostsTags' now works because of just the 2 lines below.

See: https://github.com/dotnet/efcore/issues/23703#issuecomment-758801618

/Server/Data/ApplicationDbContext.cs

    modelBuilder.Entity<Post>().Navigation(e => e.TagsInPostData).HasField("tagsInPostData_");
    modelBuilder.Entity<Tag>().Navigation(e => e.PostsInTagData).HasField("postsInTagData_");
 
