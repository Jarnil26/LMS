using System;
using System.Collections.Generic;

namespace LMS.Backend.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TeacherId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public User Teacher { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<LectureMaterial> LectureMaterials { get; set; } = new List<LectureMaterial>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
