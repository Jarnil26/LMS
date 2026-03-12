using System;
using System.Collections.Generic;

namespace LMS.Backend.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int StudentCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class CourseDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<LectureMaterialDto> Materials { get; set; }
        public List<AssignmentDto> Assignments { get; set; }
        public int StudentCount { get; set; }
    }
}
