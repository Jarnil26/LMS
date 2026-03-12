import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course, CourseDetail, CreateCourseRequest, UpdateCourseRequest, LectureMaterial, CreateLectureMaterialRequest } from '../models/course.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private apiUrl = 'http://localhost:5000/api/courses';

  constructor(private http: HttpClient, private authService: AuthService) { }

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  createCourse(request: CreateCourseRequest): Observable<any> {
    return this.http.post(this.apiUrl, request, { headers: this.getHeaders() });
  }

  getAllCourses(): Observable<any> {
    return this.http.get(this.apiUrl, { headers: this.getHeaders() });
  }

  getTeacherCourses(): Observable<any> {
    return this.http.get(`${this.apiUrl}/my-courses`, { headers: this.getHeaders() });
  }

  getStudentCourses(): Observable<any> {
    return this.http.get(`${this.apiUrl}/enrolled-courses`, { headers: this.getHeaders() });
  }

  getCourseDetails(courseId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${courseId}`, { headers: this.getHeaders() });
  }

  updateCourse(courseId: number, request: UpdateCourseRequest): Observable<any> {
    return this.http.put(`${this.apiUrl}/${courseId}`, request, { headers: this.getHeaders() });
  }

  deleteCourse(courseId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${courseId}`, { headers: this.getHeaders() });
  }

  enrollCourse(courseId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${courseId}/enroll`, {}, { headers: this.getHeaders() });
  }

  uploadMaterial(courseId: number, request: CreateLectureMaterialRequest, fileUrl: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/${courseId}/materials?fileUrl=${encodeURIComponent(fileUrl)}`, request, { headers: this.getHeaders() });
  }

  getCourseMaterials(courseId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${courseId}/materials`, { headers: this.getHeaders() });
  }

  deleteMaterial(courseId: number, materialId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${courseId}/materials/${materialId}`, { headers: this.getHeaders() });
  }
}
