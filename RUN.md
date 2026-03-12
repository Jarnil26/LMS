# LMS - How to Run

This project consists of an ASP.NET Core Backend and an Angular Frontend.

## 🚀 Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node.js (LTS)](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)

---

## 🔧 Backend Setup (LMS.Backend)

1. **Navigate to the backend folder:**
   ```bash
   cd LMS.Backend
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Database Setup:**
   - The project uses SQLite by default (`lms.db`).
   - Run migrations to initialize the database:
     ```bash
     dotnet ef database update
     ```

4. **Run the API:**
   ```bash
   dotnet run
   ```
   - API URL: `https://localhost:5001` (or `http://localhost:5000`)
   - Swagger Documentation: `https://localhost:5001/swagger`

---

## 🎨 Frontend Setup (LMS.Frontend)

1. **Navigate to the frontend folder:**
   ```bash
   cd LMS.Frontend
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Run the Development Server:**
   ```bash
   npm start
   ```
   - Application URL: `http://localhost:4200`

---

## 📝 Troubleshooting
- **CORS Issues:** Ensure the backend `appsettings.json` includes `http://localhost:4200` in `AllowedOrigins`.
- **Port Conflicts:** If `5001` or `4200` are in use, modify `launchSettings.json` (Backend) or use `ng serve --port XXXX` (Frontend).
