using System;

namespace LMS.Backend.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public string StudentId { get; set; }
        public string FileUrl { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public decimal? Grade { get; set; }
        public string Feedback { get; set; }
        public DateTime? GradedAt { get; set; }
        
        // Navigation properties
        public Assignment Assignment { get; set; }
        public User Student { get; set; }
    }
}
