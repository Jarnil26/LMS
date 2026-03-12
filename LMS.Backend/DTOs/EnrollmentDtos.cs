using System;

namespace LMS.Backend.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrolledDate { get; set; }
    }

    public class CreateEnrollmentDto
    {
        public string StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
