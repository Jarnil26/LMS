# LMS Frontend - Setup Guide

## Quick Start

### Prerequisites
- Node.js 16+ and npm
- Angular CLI: `npm install -g @angular/cli`

### Installation

1. Install dependencies:
```bash
npm install
```

2. Start development server:
```bash
npm start
```

3. Open browser at `http://localhost:4200`

## Configuration

### API URL
Update `src/environments/environment.ts`:
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

### Build for Production
```bash
npm run build
```

Output will be in `dist/LMS.Frontend`

## Project Structure

```
src/
├── app/
│   ├── components/
│   │   ├── login/
│   │   ├── register/
│   │   ├── dashboard/
│   │   ├── courses/
│   │   ├── assignments/
│   │   ├── submissions/
│   │   ├── grades/
│   │   └── profile/
│   ├── layouts/
│   │   └── sidebar/
│   ├── services/
│   │   ├── auth.service.ts
│   │   ├── course.service.ts
│   │   └── assignment.service.ts
│   ├── models/
│   │   ├── auth.model.ts
│   │   ├── course.model.ts
│   │   ├── assignment.model.ts
│   │   └── common.model.ts
│   ├── guards/
│   │   └── auth.guard.ts
│   ├── app.module.ts
│   ├── app-routing.module.ts
│   └── app.component.ts
├── assets/
├── environments/
├── styles.css
└── index.html
```

## Key Features

### Authentication
- JWT token-based auth
- Automatic token refresh
- Secure localStorage
- Route guards

### Services
- Centralized API calls
- Observable-based
- Error handling
- Type-safe

### Guards
- Auth guard for protected routes
- Role-based access control
- Auto-redirect on unauthorized

## Development Commands

```bash
# Start dev server
npm start

# Build for production
npm run build

# Run tests
npm test

# Lint code
npm run lint

# Watch mode for development
npm run watch
```

## Module Structure

Each feature has its own module:
- Lazy loading enabled
- Separate routing
- Self-contained components
- Reusable services

## Styling

- CSS Grid & Flexbox
- Mobile responsive
- Dark mode ready
- Consistent design system

## Browser Support

- Chrome/Edge (latest)
- Firefox (latest)
- Safari (latest)
- Mobile browsers

## Deployment

### Netlify
```bash
npm run build
# Deploy dist/LMS.Frontend folder
```

### Vercel
```bash
npm run build
# Deploy dist/LMS.Frontend folder
```

### Azure Static Web Apps
```bash
npm run build
# Deploy dist/LMS.Frontend folder
```

### Configure API
Set API_URL environment variable on host to backend URL.

## Testing

```bash
# Unit tests
npm test

# E2E tests  
npm run e2e

# Code coverage
npm run test -- --code-coverage
```

## Performance

- Lazy-loaded modules
- OnPush change detection
- Tree-shakeable code
- Optimized bundle size

## Security

- XSS protection
- CSRF token handling
- Secure headers
- Content Security Policy

---

For full documentation, see README.md in project root.
