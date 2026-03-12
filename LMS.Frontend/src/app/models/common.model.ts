export interface DashboardStats {
  totalCourses: number;
  totalStudents: number;
  pendingAssignments: number;
  completedSubmissions: number;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}
