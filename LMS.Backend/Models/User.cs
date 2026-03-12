using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LMS.Backend.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; } // Admin, Teacher, Student
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public ICollection<Course> CoursesCreated { get; set; } = new List<Course>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
