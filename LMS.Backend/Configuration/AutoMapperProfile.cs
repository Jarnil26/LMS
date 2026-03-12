using AutoMapper;
using LMS.Backend.DTOs;
using LMS.Backend.Models;

namespace LMS.Backend.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();

            // Course mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.FullName));
            CreateMap<Course, CourseDetailDto>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.FullName));
            CreateMap<CreateCourseDto, Course>();

            // LectureMaterial mappings
            CreateMap<LectureMaterial, LectureMaterialDto>();
            CreateMap<CreateLectureMaterialDto, LectureMaterial>();

            // Assignment mappings
            CreateMap<Assignment, AssignmentDto>()
                .ForMember(dest => dest.SubmissionCount, opt => opt.MapFrom(src => src.Submissions.Count));
            CreateMap<Assignment, AssignmentDetailDto>()
                .ForMember(dest => dest.SubmissionCount, opt => opt.MapFrom(src => src.Submissions.Count));
            CreateMap<CreateAssignmentDto, Assignment>();

            // Submission mappings
            CreateMap<Submission, SubmissionDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
                .ForMember(dest => dest.AssignmentTitle, opt => opt.MapFrom(src => src.Assignment.Title))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Assignment.Course.Title))
                .ForMember(dest => dest.IsGraded, opt => opt.MapFrom(src => src.Grade.HasValue))
                .ForMember(dest => dest.SubmissionDate, opt => opt.MapFrom(src => src.SubmittedAt));
            CreateMap<CreateSubmissionDto, Submission>();

            // Enrollment mappings
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<CreateEnrollmentDto, Enrollment>();
        }
    }
}
