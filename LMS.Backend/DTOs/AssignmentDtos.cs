using System;
using System.Collections.Generic;

namespace LMS.Backend.DTOs
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int SubmissionCount { get; set; }
        public string CourseName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateAssignmentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class AssignmentDetailDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SubmissionCount { get; set; }
        public List<SubmissionDto> Submissions { get; set; }
    }
}
