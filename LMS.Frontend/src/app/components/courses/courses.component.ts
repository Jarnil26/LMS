import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { AuthService } from '../../services/auth.service';
import { Course } from '../../models/course.model';
import { User } from '../../models/auth.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {
  courses: Course[] = [];
  currentUser: User | null = null;
  loading = true;
  error = '';
  showModal = false;
  isBrowsing = false;
  courseForm: FormGroup;

  constructor(
    private courseService: CourseService,
    private authService: AuthService,
    private fb: FormBuilder
  ) { 
    this.courseForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      category: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUserValue();
    this.loadCourses();
  }

  loadCourses(): void {
    this.loading = true;
    let loadObservable;

    if (this.currentUser?.role === 'Teacher') {
      loadObservable = this.courseService.getTeacherCourses();
    } else if (this.isBrowsing) {
      loadObservable = this.courseService.getAllCourses();
    } else {
      loadObservable = this.courseService.getStudentCourses();
    }

    loadObservable.subscribe({
      next: (response) => {
        if (response.success) {
          this.courses = response.data;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load courses';
        this.loading = false;
      }
    });
  }

  toggleBrowsing(): void {
    this.isBrowsing = !this.isBrowsing;
    this.loadCourses();
  }

  enroll(courseId: number): void {
    this.loading = true;
    this.courseService.enrollCourse(courseId).subscribe({
      next: (response) => {
        if (response.success) {
          this.isBrowsing = false;
          this.loadCourses();
        } else {
          this.error = response.message || 'Failed to enroll';
          this.loading = false;
        }
      },
      error: () => {
        this.error = 'An error occurred during enrollment';
        this.loading = false;
      }
    });
  }

  toggleModal(): void {
    this.showModal = !this.showModal;
    if (!this.showModal) {
      this.courseForm.reset();
      this.error = '';
    }
  }

  onSubmit(): void {
    if (this.courseForm.valid) {
      this.loading = true;
      this.courseService.createCourse(this.courseForm.value).subscribe({
        next: (response) => {
          if (response.success) {
            this.toggleModal();
            this.loadCourses();
          } else {
            this.error = response.message || 'Failed to create course';
            this.loading = false;
          }
        },
        error: (err) => {
          this.error = 'An error occurred while creating the course';
          this.loading = false;
        }
      });
    }
  }
}
