using System;
using System.Collections.Generic;

namespace LMS.Backend.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public Course Course { get; set; }
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
