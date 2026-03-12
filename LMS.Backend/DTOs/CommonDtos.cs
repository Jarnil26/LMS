using System;

namespace LMS.Backend.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalCourses { get; set; }
        public int TotalStudents { get; set; }
        public int PendingAssignments { get; set; }
        public int CompletedSubmissions { get; set; }
    }

    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ErrorResponseDto
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public string[] Errors { get; set; }
    }
}
