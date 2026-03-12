# LMS Backend - Setup Guide

## Quick Start

### Prerequisites
- .NET 6.0 SDK or later
- Supabase account
- Visual Studio 2022 or VS Code

### Step 1: Configure Database

1. Create a Supabase project at https://supabase.com
2. Get your connection string from Project Settings > Database
3. Update `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=your-host;Port=5432;Database=your-db;Username=postgres;Password=your-password;SSL Mode=Require;"
}
```

### Step 2: Install EF Core Tools

```bash
dotnet tool install --global dotnet-ef
```

### Step 3: Create Database

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Step 4: Configure JWT

Update `appsettings.json` with a strong secret key (min 32 characters):

```json
"JwtSettings": {
  "SecretKey": "your-super-secret-key-min-32-characters-recommended",
  "Issuer": "LMS.Backend",
  "Audience": "LMS.Frontend",
  "ExpirationMinutes": 1440
}
```

### Step 5: Run Application

```bash
dotnet run
```

Access Swagger UI at: `https://localhost:5001/swagger`

## API Testing

### Register User
```bash
POST /api/auth/register
Content-Type: application/json

{
  "fullName": "John Doe",
  "email": "john@example.com",
  "password": "password123",
  "confirmPassword": "password123",
  "role": "Student"
}
```

### Login
```bash
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "password123"
}
```

## Architecture

### Clean Architecture Layers
1. **Controllers** - HTTP endpoints
2. **DTOs** - Data contracts
3. **Services** - Business logic
4. **Repositories** - Data access
5. **Models** - Domain entities
6. **Data** - EF Core configuration

### Key Classes
- `ApplicationDbContext` - EF Core DbContext
- `AuthService` - Authentication & JWT
- `CourseService` - Course management
- `AssignmentService` - Assignment management
- `SubmissionService` - Submission handling

## Database Seeding

Add sample data in `Program.cs` after migrations:

```csharp
// After app.Build()
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Add seed data
}
```

## Environment Variables

Create `.env` file or use user secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
dotnet user-secrets set "JwtSettings:SecretKey" "your-secret-key"
```

## Troubleshooting

### Database Connection Failed
- Check connection string format
- Verify Supabase is running
- Test with pgAdmin or psql

### Migration Issues
```bash
# Remove last migration
dotnet ef migrations remove

# View pending migrations
dotnet ef migrations list

# Update to specific migration
dotnet ef database update MigrationName
```

### Port Already in Use
Edit `launchSettings.json` to use different port

## Production Deployment

1. Build release: `dotnet build -c Release`
2. Publish: `dotnet publish -c Release -o ./publish`
3. Deploy to Azure App Service, AWS, or other host
4. Set environment variables on host
5. Ensure database backups are configured

---

For API documentation, see README.md in project root.
