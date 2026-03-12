using System;

namespace LMS.Backend.Models
{
    public class LectureMaterial
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public Course Course { get; set; }
    }
}
