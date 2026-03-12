using System;

namespace LMS.Backend.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrolledDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public User Student { get; set; }
        public Course Course { get; set; }
    }
}
