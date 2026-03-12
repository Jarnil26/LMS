using System;

namespace LMS.Backend.DTOs
{
    public class LectureMaterialDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
    }

    public class CreateLectureMaterialDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileType { get; set; }
    }
}
