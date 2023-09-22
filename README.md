# Social Media Application

The Social Media Application is a simple social media platform built using .NET 7, MinimalAPI, Dapper, and PostgreSQL. This application allows users to create and view posts, follow other users, and like posts. Each post has a title, body, and author.
***Written entirely using ChatGPT 3.5 (even this file).***

## Prerequisites

Before running the application, make sure you have the following installed:
- .NET 7 SDK
- PostgreSQL database server

## Database Setup

1. Create a PostgreSQL database.
2. Run the SQL script `create_tables.sql` located in the solution directory to create the required tables in your database.

## Configuration

1. In the `appsettings.json` file of the API project, update the `"DefaultConnection"` connection string with your PostgreSQL database connection details.

## Running the Application

1. Open a terminal or command prompt.
2. Navigate to the root directory of the API project.
3. Run the following commands:

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

The API should now be running locally, and you can access it at `http://localhost:5000`.

### Endpoints

- **Create a New Post**: `POST /posts`
  - Create a new post by providing a JSON object with `title`, `body`, and `authorId` in the request body.

- **View Posts by User**: `GET /user/{userId}/posts`
  - Get a list of posts authored by a specific user by replacing `{userId}` with the user's ID.

- **Follow a User**: `POST /user/{followerId}/follow/{followingId}`
  - Make a user follow another user by replacing `{followerId}` and `{followingId}` with the respective user IDs.

- **Like a Post**: `POST /likepost/{userId}/{postId}`
  - Like a post by a user by replacing `{userId}` and `{postId}` with the respective user and post IDs.

Please make sure to replace `{userId}` and `{postId}` with actual user and post IDs when making requests.

Enjoy using the Social Media Application!

## Short feedback
- Was it easy to complete the task using AI?
	- `That was hard.`
- How long did task take you to complete? (Please be honest, we need it to gather anonymized statistics)
	- `Around 12 hours...`
- Was the code ready to run after generation? What did you have to change to make it usable?
	- `Most of the time it was okay. But ChatGPT did not provide complete solution right away. For example i had to ask him about exception handling and some clean code violations.`
- Which challenges did you face during completion of the task?
	- `I haven't used Dapper and the minimal API before. But the main challenge was that ChatGPT knows a little about minimal API, because it was introduced in 2022.`
- Which specific prompts you learned as a good practice to complete the task?
	- `"... Use styled markdown for the output."`,
	- `Generate SQL queries to create database schema for this application, according to the models provided.`,
	- `Does this class follow SOLID principles?`

