export interface Assignment {
  id: number;
  courseId: number;
  title: string;
  description: string;
  dueDate: Date;
  submissionCount: number;
  courseName: string;
  createdAt: Date;
}

export interface AssignmentDetail extends Assignment {
  submissions: Submission[];
}

export interface CreateAssignmentRequest {
  title: string;
  description: string;
  dueDate: Date;
}

export interface Submission {
  id: number;
  assignmentId: number;
  studentId: string;
  studentName: string;
  fileUrl: string;
  submittedAt: Date;
  submissionDate: Date; // Helper for display
  assignmentTitle: string;
  courseName: string;
  grade?: number;
  feedback?: string;
  gradedAt?: Date;
  isGraded: boolean;
}

export interface CreateSubmissionRequest {
  fileUrl: string;
}

export interface GradeSubmissionRequest {
  grade: number;
  feedback: string;
}
