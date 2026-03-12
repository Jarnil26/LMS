import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Assignment, AssignmentDetail, CreateAssignmentRequest, Submission, CreateSubmissionRequest, GradeSubmissionRequest } from '../models/assignment.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AssignmentService {
  private apiUrl = 'http://localhost:5000/api/courses';

  constructor(private http: HttpClient, private authService: AuthService) { }

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  createAssignment(courseId: number, request: CreateAssignmentRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/${courseId}/assignments`, request, { headers: this.getHeaders() });
  }

  getCourseAssignments(courseId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${courseId}/assignments`, { headers: this.getHeaders() });
  }

  getAssignmentDetails(courseId: number, assignmentId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${courseId}/assignments/${assignmentId}`, { headers: this.getHeaders() });
  }

  updateAssignment(courseId: number, assignmentId: number, request: CreateAssignmentRequest): Observable<any> {
    return this.http.put(`${this.apiUrl}/${courseId}/assignments/${assignmentId}`, request, { headers: this.getHeaders() });
  }

  deleteAssignment(courseId: number, assignmentId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${courseId}/assignments/${assignmentId}`, { headers: this.getHeaders() });
  }

  submitAssignment(assignmentId: number, request: CreateSubmissionRequest): Observable<any> {
    return this.http.post(`http://localhost:5000/api/assignments/${assignmentId}/submissions`, request, { headers: this.getHeaders() });
  }

  getAssignmentSubmissions(assignmentId: number): Observable<any> {
    return this.http.get(`http://localhost:5000/api/assignments/${assignmentId}/submissions`, { headers: this.getHeaders() });
  }

  getMySubmissions(): Observable<any> {
    return this.http.get(`http://localhost:5000/api/submissions/my-submissions`, { headers: this.getHeaders() });
  }

  getMyAssignments(): Observable<any> {
    return this.http.get(`http://localhost:5000/api/assignments/my-assignments`, { headers: this.getHeaders() });
  }

  getSubmission(assignmentId: number, submissionId: number): Observable<any> {
    return this.http.get(`http://localhost:5000/api/assignments/${assignmentId}/submissions/${submissionId}`, { headers: this.getHeaders() });
  }

  gradeSubmission(assignmentId: number, submissionId: number, request: GradeSubmissionRequest): Observable<any> {
    return this.http.put(`http://localhost:5000/api/assignments/${assignmentId}/submissions/${submissionId}/grade`, request, { headers: this.getHeaders() });
  }
}
