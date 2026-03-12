# LMS Quick Start Guide

## 🚀 5-Minute Setup

### What You'll Have
A complete Learning Management System with:
- ✅ User authentication & role-based access
- ✅ Secure REST API with JWT
- ✅ Professional Angular dashboard
- ✅ Database with Supabase
- ✅ Teacher & student features

---

## Backend Setup (ASP.NET Core)

### 1. Create Supabase Database
1. Visit https://supabase.com and create account
2. Create new project
3. Go to Settings > Database > Connection String
4. Copy the connection string

### 2. Configure Backend
```bash
cd LMS.Backend
```

Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=YOUR_HOST;Port=5432;Database=YOUR_DB;Username=postgres;Password=YOUR_PASSWORD;SSL Mode=Require;"
  },
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-must-be-at-least-32-characters-long",
    "Issuer": "LMS.Backend",
    "Audience": "LMS.Frontend",
    "ExpirationMinutes": 1440
  }
}
```

### 3. Create Database
```bash
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run Backend
```bash
dotnet run
```

**Backend running at**: `https://localhost:5001`
**Swagger API Docs**: `https://localhost:5001/swagger`

---

## Frontend Setup (Angular)

### 1. Install Dependencies
```bash
cd LMS.Frontend
npm install
```

### 2. Verify API URL
Check `src/environments/environment.ts`:
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

### 3. Run Frontend
```bash
npm start
```

**Frontend running at**: `http://localhost:4200`

---

## Test the Application

### 1. Register New User
- Go to http://localhost:4200
- Click "Create one" (Register)
- Fill in details:
  - Full Name: John Doe
  - Email: john@example.com
  - Password: Test123!
  - Role: Student

### 2. Login
- Use your registered credentials
- Should redirect to Dashboard

### 3. Create a Course (As Teacher)
- Register as Teacher role
- Click "Courses" in sidebar
- Click "Create Course"
- Fill in title and description
- Submit

### 4. Enroll in Course (As Student)
- Register as Student
- Go to Courses
- Find available courses
- Click "Enroll"

---

## Project Structure at a Glance

```
LMS/
├── LMS.Backend/                # ASP.NET Core API
│   ├── Models/                 # Database entities
│   ├── Services/               # Business logic
│   ├── Controllers/            # API endpoints
│   ├── Data/                   # EF Core DbContext
│   ├── appsettings.json       # Configuration
│   └── LMS.Backend.csproj
│
└── LMS.Frontend/               # Angular app
    ├── src/app/
    │   ├── components/         # Pages
    │   ├── services/           # API calls
    │   ├── models/             # Interfaces
    │   └── guards/             # Route protection
    ├── package.json
    └── angular.json
```

---

## API Endpoints Summary

### Authentication
```
POST   /api/auth/register        - Create account
POST   /api/auth/login           - Login
PUT    /api/auth/profile         - Update profile
GET    /api/auth/current-user    - Get user info
```

### Courses
```
GET    /api/courses/my-courses       - Teacher's courses
GET    /api/courses/enrolled-courses - Student's courses
POST   /api/courses                  - Create course
GET    /api/courses/{id}             - Get details
PUT    /api/courses/{id}             - Update
DELETE /api/courses/{id}             - Delete
POST   /api/courses/{id}/enroll      - Enroll student
```

### Assignments
```
GET    /api/courses/{cId}/assignments           - List
POST   /api/courses/{cId}/assignments           - Create
GET    /api/courses/{cId}/assignments/{id}      - Details
PUT    /api/courses/{cId}/assignments/{id}      - Update
DELETE /api/courses/{cId}/assignments/{id}      - Delete
```

### Submissions
```
POST   /api/assignments/{aId}/submissions           - Submit
GET    /api/assignments/{aId}/submissions           - List
GET    /api/assignments/{aId}/submissions/{id}      - Details
PUT    /api/assignments/{aId}/submissions/{id}/grade - Grade
```

---

## User Roles

### Student
- View enrolled courses
- Submit assignments
- View grades
- Download materials

### Teacher
- Create courses
- Manage assignments
- Grade submissions
- View students
- Upload materials

### Admin
- Manage users
- Assign roles
- System settings
- Monitor platform

---

## Troubleshooting

### Backend Won't Start
```bash
# Check ports
netstat -ano | findstr :5001

# Clear cache
dotnet clean
dotnet restore
dotnet run
```

### Database Connection Error
- Verify Supabase project is active
- Check connection string in appsettings.json
- Ensure network access is allowed in Supabase

### Frontend CORS Error
- Ensure backend is running
- Check API_URL in environment.ts
- Verify CORS in appsettings.json

### Port Conflicts
- Backend: Edit `launchSettings.json` to change port
- Frontend: `ng serve --port 4300`

---

## Next Steps

### Customization
1. Update logo in sidebar.component.html
2. Change colors in component CSS files
3. Modify dashboard widgets
4. Add custom fields to models

### Database Seeding
Add test data in `Program.cs`:
```csharp
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
// Add seed data here
context.SaveChanges();
```

### Deployment
- **Backend**: Azure App Service, AWS EC2, DigitalOcean
- **Frontend**: Netlify, Vercel, Azure Static Web Apps
- **Database**: Supabase (already hosted)

---

## Files to Modify

### Database Connection
- `LMS.Backend/appsettings.json` - Add your Supabase connection string

### JWT Security
- `LMS.Backend/appsettings.json` - Change SecretKey to unique value

### API URL
- `LMS.Frontend/src/environments/environment.ts` - Verify backend URL

### CORS Origins
- `LMS.Backend/Startup.cs` - Update AllowedOrigins array

---

## Key Technologies

| Layer | Technology | Purpose |
|-------|-----------|---------|
| Frontend | Angular 16 | UI framework |
| Backend | ASP.NET Core 6 | API server |
| Database | PostgreSQL/Supabase | Data storage |
| Auth | JWT | Token-based auth |
| ORM | EF Core | Database access |
| Styling | CSS3 | Responsive design |

---

## Performance Tips

1. **Enable Caching**
   - Add HTTP caching headers
   - Cache frequently accessed data

2. **Database Optimization**
   - Add indexes on foreign keys
   - Use query eager loading

3. **Frontend Optimization**
   - Lazy load modules
   - Minify and compress assets
   - Use OnPush change detection

4. **API Optimization**
   - Implement pagination
   - Add response compression
   - Use async/await

---

## Security Checklist

- [ ] Change JWT secret key
- [ ] Update CORS origins
- [ ] Enable HTTPS in production
- [ ] Set secure headers
- [ ] Implement rate limiting
- [ ] Add input validation
- [ ] Use environment variables
- [ ] Enable database SSL
- [ ] Regular security updates

---

## Support Resources

- **ASP.NET Core Docs**: https://docs.microsoft.com/dotnet/core
- **Angular Docs**: https://angular.io/docs
- **PostgreSQL Docs**: https://www.postgresql.org/docs
- **Supabase Docs**: https://supabase.com/docs
- **Entity Framework Core**: https://docs.microsoft.com/ef/core

---

## Common Commands

```bash
# Backend
dotnet new webapi -n MyProject       # Create new project
dotnet add package PackageName       # Add NuGet package
dotnet ef migrations add Name        # Create migration
dotnet ef database update            # Apply migrations
dotnet run                          # Run application

# Frontend
ng new project-name                 # Create new Angular app
ng generate component name          # Create component
ng generate service name            # Create service
ng build                           # Build for production
npm start                          # Run dev server
```

---

## Production Checklist

### Before Deploying
- [ ] Run unit tests
- [ ] Check for console errors
- [ ] Verify all API endpoints
- [ ] Test user authentication
- [ ] Check responsive design
- [ ] Validate database backups
- [ ] Review security settings
- [ ] Update dependencies

### Deployment Steps
1. Build: `dotnet build -c Release` & `npm run build`
2. Test in staging environment
3. Configure production database
4. Set environment variables
5. Deploy backend then frontend
6. Test all features
7. Monitor error logs
8. Enable analytics

---

**Version**: 1.0.0
**Last Updated**: March 2026

For detailed documentation, see [README.md](README.md)
