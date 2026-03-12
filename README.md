# Learning Management System (LMS) - Complete Documentation

## Project Overview

A professional, full-featured Learning Management System built with clean architecture principles. This system enables teachers to manage courses and assignments while students can enroll, submit work, and view grades.

## Technology Stack

### Backend
- **Framework**: ASP.NET Core 6.0
- **Database**: Supabase (PostgreSQL)
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Identity + JWT
- **Authorization**: Role-based Access Control (RBAC)
- **API**: RESTful Web API
- **Mapping**: AutoMapper
- **Dependency Injection**: Built-in ASP.NET Core DI

### Frontend
- **Framework**: Angular 16+
- **Language**: TypeScript
- **Styling**: CSS3 with responsive design
- **State Management**: RxJS Observables
- **HTTP Client**: Angular HttpClient
- **Authentication**: JWT Tokens
- **Routing**: Angular Router with Guards

### Database
- **Provider**: Supabase (PostgreSQL)
- **Cloud Hosting**: Yes
- **SSL/TLS**: Required

## Project Structure

```
LMS/
├── LMS.Backend/
│   ├── Controllers/          # API endpoints
│   ├── Models/               # Entity models
│   ├── DTOs/                 # Data Transfer Objects
│   ├── Services/             # Business logic
│   ├── Repositories/         # Data access layer
│   ├── Data/                 # DbContext and configuration
│   ├── Configuration/        # AutoMapper profiles
│   ├── Authentication/       # Auth utilities
│   ├── Middlewares/          # Exception handling, etc.
│   ├── Migrations/           # EF Core migrations
│   ├── appsettings.json      # Configuration
│   ├── Program.cs            # Application entry point
│   ├── Startup.cs            # Service configuration
│   └── LMS.Backend.csproj    # Project file
│
└── LMS.Frontend/
    ├── src/
    │   ├── app/
    │   │   ├── components/        # Feature components
    │   │   ├── layouts/           # Layout components
    │   │   ├── services/          # HTTP services
    │   │   ├── models/            # TypeScript interfaces
    │   │   ├── guards/            # Route guards
    │   │   ├── app.module.ts      # Main module
    │   │   ├── app-routing.module.ts
    │   │   ├── app.component.ts
    │   │   └── app.component.html
    │   ├── assets/                # Images, fonts, etc.
    │   ├── environments/          # Environment configs
    │   ├── styles.css             # Global styles
    │   ├── index.html
    │   └── main.ts
    ├── angular.json
    ├── package.json
    ├── tsconfig.json
    └── README.md
```

## Database Schema

### Users Table
```sql
- Id (PK)
- UserName
- Email
- PasswordHash
- FullName
- Role (Admin, Teacher, Student)
- CreatedAt
- UpdatedAt
```

### Courses Table
```sql
- Id (PK)
- Title
- Description
- TeacherId (FK)
- CreatedAt
- UpdatedAt
```

### Enrollments Table
```sql
- Id (PK)
- StudentId (FK)
- CourseId (FK)
- EnrolledDate
```

### LectureMaterials Table
```sql
- Id (PK)
- CourseId (FK)
- Title
- FileUrl
- FileType
- Description
- UploadDate
```

### Assignments Table
```sql
- Id (PK)
- CourseId (FK)
- Title
- Description
- DueDate
- CreatedAt
- UpdatedAt
```

### Submissions Table
```sql
- Id (PK)
- AssignmentId (FK)
- StudentId (FK)
- FileUrl
- SubmittedAt
- Grade
- Feedback
- GradedAt
```

## API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `PUT /api/auth/profile` - Update profile (Authenticated)
- `GET /api/auth/current-user` - Get current user (Authenticated)

### Courses
- `GET /api/courses/my-courses` - Get teacher's courses (Teacher)
- `GET /api/courses/enrolled-courses` - Get student's courses (Student)
- `GET /api/courses/{id}` - Get course details
- `POST /api/courses` - Create course (Teacher)
- `PUT /api/courses/{id}` - Update course (Teacher)
- `DELETE /api/courses/{id}` - Delete course (Teacher)
- `POST /api/courses/{courseId}/enroll` - Enroll in course (Student)

### Assignments
- `GET /api/courses/{courseId}/assignments` - Get course assignments
- `GET /api/courses/{courseId}/assignments/{id}` - Get assignment details
- `POST /api/courses/{courseId}/assignments` - Create assignment (Teacher)
- `PUT /api/courses/{courseId}/assignments/{id}` - Update assignment (Teacher)
- `DELETE /api/courses/{courseId}/assignments/{id}` - Delete assignment (Teacher)

### Submissions
- `POST /api/assignments/{assignmentId}/submissions` - Submit assignment (Student)
- `GET /api/assignments/{assignmentId}/submissions` - Get submissions
- `GET /api/assignments/{assignmentId}/submissions/{id}` - Get submission details
- `PUT /api/assignments/{assignmentId}/submissions/{id}/grade` - Grade submission (Teacher)

### Lecture Materials
- `GET /api/courses/{courseId}/materials` - Get course materials
- `POST /api/courses/{courseId}/materials` - Upload material (Teacher)
- `DELETE /api/courses/{courseId}/materials/{materialId}` - Delete material (Teacher)

## Setup Instructions

### Backend Setup

1. **Clone and navigate to backend**
```bash
cd LMS.Backend
```

2. **Install dependencies**
```bash
dotnet restore
```

3. **Configure Database**
   - Update `appsettings.json` with your Supabase connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=your-supabase-host;Port=5432;Database=your-db;Username=your-user;Password=your-pass;SSL Mode=Require;"
   }
   ```

4. **Update JWT Settings** in `appsettings.json`
   - Replace the `SecretKey` with a strong 32+ character key

5. **Create Database Migrations**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

6. **Run the Backend**
```bash
dotnet run
```

The API will be available at `https://localhost:5001`

### Frontend Setup

1. **Clone and navigate to frontend**
```bash
cd LMS.Frontend
```

2. **Install dependencies**
```bash
npm install
```

3. **Update API Configuration**
   - Check `src/environments/environment.ts` for API URL

4. **Run the Frontend**
```bash
npm start
```

The application will be available at `http://localhost:4200`

## User Roles and Permissions

### Admin
- Manage all users
- Assign roles
- Monitor all courses
- System administration

### Teacher
- Create and manage courses
- Upload lecture materials
- Create assignments
- View and grade student submissions
- View course statistics

### Student
- Enroll in courses
- View course materials
- Submit assignments
- View grades and feedback
- Access personal dashboard

## Key Features

### Dashboard
- Role-specific dashboards
- Quick statistics
- Recent courses
- Assignment deadlines
- Performance metrics

### Course Management
- Create/edit/delete courses
- Course descriptions
- Student enrollment
- Course materials library

### Assignment System
- Assignment creation with deadlines
- File submissions
- Submission tracking
- Grading interface
- Feedback system

### Authentication
- Secure JWT-based authentication
- Role-based authorization
- Session management
- Automatic token refresh

### User Interface
- Professional SaaS-style design
- Responsive layout
- Sidebar navigation
- Card-based components
- Color-coded elements
- Smooth animations

## Design System

### Color Palette
- **Primary**: #2563EB (Blue)
- **Secondary**: #0F172A (Dark Navy)
- **Success**: #22C55E (Green)
- **Danger**: #EF4444 (Red)
- **Background**: #F8FAFC (Light Gray)
- **Border**: #E2E8F0 (Light Gray)
- **Text**: #0F172A (Dark)
- **Muted**: #64748B (Medium Gray)

### Typography
- **Font Family**: Inter, -apple-system, BlinkMacSystemFont
- **Headings**: Bold (600-700)
- **Body**: Regular (400-500)
- **Small**: 12-13px

### Spacing
- Uses 8px grid system
- Consistent padding/margins
- Responsive gaps

## Security Features

1. **Authentication**
   - JWT tokens with expiration
   - Secure password hashing
   - Password confirmation on registration

2. **Authorization**
   - Role-based access control
   - Route guards
   - API endpoint protection

3. **Data Protection**
   - HTTPS/TLS required
   - SQL injection prevention via EF Core
   - CORS configuration
   - Input validation

4. **Best Practices**
   - No sensitive data in localStorage (JWT only)
   - Secure token storage
   - Automatic token cleanup on logout

## Deployment

### Backend (Azure, AWS, etc.)
1. Build release: `dotnet build -c Release`
2. Deploy to host with .NET 6+ runtime
3. Configure environment variables for database
4. Ensure HTTPS is enabled

### Frontend (Netlify, Vercel, Azure Static Web Apps)
1. Build: `npm run build`
2. Deploy `dist/LMS.Frontend` folder
3. Configure API proxy for backend calls
4. Enable CORS on backend for frontend domain

## Database Configuration

### Supabase Connection String Template
```
Host=db.{project-ref}.supabase.co
Port=5432
Database=postgres
Username=postgres
Password=your-password
SSL Mode=Require
```

### Creating Supabase Account
1. Go to https://supabase.com
2. Create project
3. Get connection details from Database settings
4. Copy to appsettings.json

## Future Enhancements

- Video streaming integration
- Real-time notifications
- Discussion forums
- Peer reviews
- Advanced analytics
- Mobile app
- Calendar integration
- Email notifications
- Attendance tracking
- Quiz/exam module
- Certificate generation

## Troubleshooting

### Common Issues

**Backend Connection Error**
- Check connection string in appsettings.json
- Verify Supabase database is running
- Ensure firewall allows connections

**CORS Errors**
- Check AllowedOrigins in appsettings.json
- Verify frontend URL matches configuration
- Clear browser cache

**Authentication Issues**
- Verify tokens are being stored
- Check JWT secret key is correct
- Ensure expiration time is sufficient

**Migration Errors**
- Delete previous migrations if needed
- `dotnet ef migrations remove`
- Create fresh: `dotnet ef migrations add InitialCreate`

## Git Workflow

```bash
# Clone repo
git clone <repo-url>

# Create feature branch
git checkout -b feature/your-feature

# Make changes and commit
git add .
git commit -m "feat: your feature description"

# Push and create pull request
git push origin feature/your-feature
```

## Testing

### Backend Tests
```bash
dotnet test
```

### Frontend Tests
```bash
npm test
```

## Performance Optimization

- Lazy loading of Angular modules
- Async database operations
- Efficient queries with EF Core
- Pagination for large datasets
- Caching strategies
- Image optimization
- Code splitting

## Support and Documentation

For detailed API documentation, visit the Swagger UI at:
`https://localhost:5001/swagger`

## License

This project is built for educational purposes.

## Contributors

- Project Lead: [Your Name]
- Team Members: [List]

---

**Last Updated**: March 2026
**Version**: 1.0.0
