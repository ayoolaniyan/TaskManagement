# TaskManagement
A simple .NET Web API for managing Users and Tasks. Tasks areassigned to users every 2 minutes.

## Features & Logic
- Create and read Users and Tasks
- Automatically assign users to tasks on creation
- Reassign tasks to different users every two minutes
- Track user's tasks assignment history
- Ensure all users are assigned all tasks

## TODO & Improvement
- Create write unit test cases
- Exception Handling
- Track when a user was first added, to fair assigning of task
- Improve performance with EF core

## Running the application

```bash
dotnet restore
dotnet run
```

Navigate to Swagger UI at `https://localhost:{PORT}/swagger`
