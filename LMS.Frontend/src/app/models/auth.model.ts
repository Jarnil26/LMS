export interface User {
  id: string;
  fullName: string;
  email: string;
  role: 'Admin' | 'Teacher' | 'Student';
  createdAt: Date;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  fullName: string;
  email: string;
  password: string;
  confirmPassword: string;
  role: string;
}

export interface LoginResponse {
  token: string;
  user: User;
}

export interface UpdateProfileDto {
  fullName: string;
  email: string;
}
