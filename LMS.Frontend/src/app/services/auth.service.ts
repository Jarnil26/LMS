import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { User, LoginRequest, RegisterRequest, LoginResponse, UpdateProfileDto } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/api/auth';
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();
  private tokenSubject = new BehaviorSubject<string | null>(null);
  public token$ = this.tokenSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadFromLocalStorage();
  }

  register(request: RegisterRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, request).pipe(
      tap((response: any) => {
        if (response.success && response.data) {
          this.setAuth(response.data.user, response.data.token);
        }
      })
    );
  }

  login(request: LoginRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, request).pipe(
      tap((response: any) => {
        if (response.success && response.data) {
          this.setAuth(response.data.user, response.data.token);
        }
      })
    );
  }

  logout(): void {
    this.currentUserSubject.next(null);
    this.tokenSubject.next(null);
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  updateProfile(request: UpdateProfileDto): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.getToken()}`
    });
    return this.http.put(`${this.apiUrl}/profile`, request, { headers });
  }

  getCurrentUser(): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.getToken()}`
    });
    return this.http.get(`${this.apiUrl}/current-user`, { headers });
  }

  private setAuth(user: User, token: string): void {
    this.currentUserSubject.next(user);
    this.tokenSubject.next(token);
    localStorage.setItem('token', token);
    localStorage.setItem('user', JSON.stringify(user));
  }

  private loadFromLocalStorage(): void {
    const token = localStorage.getItem('token');
    const user = localStorage.getItem('user');
    
    if (token && user) {
      try {
        this.currentUserSubject.next(JSON.parse(user));
        this.tokenSubject.next(token);
      } catch (e) {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
      }
    }
  }

  getToken(): string | null {
    return this.tokenSubject.value || localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getCurrentUserValue(): User | null {
    return this.currentUserSubject.value;
  }
}
