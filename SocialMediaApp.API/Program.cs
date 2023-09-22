using SocialMediaApp.API.Data;
using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;
using SocialMediaApp.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IDapperWrapper, DapperWrapper>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<IUserRelationshipRepository, UserRelationshipRepository>();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp
    => exceptionHandlerApp.Run(async context
        => await Results.Problem()
                     .ExecuteAsync(context)));

// Create Post
app.MapPost("/posts", async (Post post, IPostService postService) =>
{
    await postService.CreatePostAsync(post);
    return Results.Created("/posts", null);
});

// View Posts by User
app.MapGet("/user/{userId}/posts", async (int userId, IPostService postService) =>
{
    var posts = await postService.GetPostsByUserIdAsync(userId);
    return Results.Json(posts);
});

// Follow User
app.MapPost("/user/{followerId}/follow/{followingId}", async (int followerId, int followingId, IUserService userService) =>
{
    await userService.FollowUserAsync(followerId, followingId);
    return Results.NoContent();
});

// Like Post
app.MapPost("/likepost/{userId}/{postId}", async (int userId, int postId, ILikeService likeService) =>
{
    await likeService.LikePostAsync(userId, postId);
    return Results.NoContent();
});

app.Run();
