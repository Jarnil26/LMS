using System;

namespace LMS.Backend.DTOs
{
    public class SubmissionDto
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string FileUrl { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string AssignmentTitle { get; set; }
        public string CourseName { get; set; }
        public bool IsGraded { get; set; }
        public decimal? Grade { get; set; }
        public string Feedback { get; set; }
        public DateTime? GradedAt { get; set; }
    }

    public class CreateSubmissionDto
    {
        public string FileUrl { get; set; }
    }

    public class GradeSubmissionDto
    {
        public decimal Grade { get; set; }
        public string Feedback { get; set; }
    }
}
