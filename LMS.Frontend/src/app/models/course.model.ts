import { Assignment, Submission } from './assignment.model';

export interface Course {
  id: number;
  title: string;
  description: string;
  teacherId: string;
  teacherName: string;
  studentCount: number;
  category: string;
  createdAt: Date;
}

export interface CourseDetail extends Course {
  materials: LectureMaterial[];
  assignments: Assignment[];
}

export interface CreateCourseRequest {
  title: string;
  description: string;
}

export interface UpdateCourseRequest {
  title: string;
  description: string;
}

export interface LectureMaterial {
  id: number;
  courseId: number;
  title: string;
  fileUrl: string;
  fileType: string;
  description: string;
  uploadDate: Date;
}

export interface CreateLectureMaterialRequest {
  title: string;
  description: string;
  fileType: string;
}
