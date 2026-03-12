using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LMS.Backend.DTOs;
using LMS.Backend.Models;
using LMS.Backend.Repositories;

namespace LMS.Backend.Services
{
    public interface ICourseService
    {
        Task<(bool Success, string Message, CourseDto Data)> CreateCourseAsync(CreateCourseDto dto, string teacherId);
        Task<(bool Success, string Message, IEnumerable<CourseDto> Data)> GetTeacherCoursesAsync(string teacherId);
        Task<(bool Success, string Message, IEnumerable<CourseDto> Data)> GetStudentCoursesAsync(string studentId);
        Task<(bool Success, string Message, IEnumerable<CourseDto> Data)> GetAllCoursesAsync();
        Task<(bool Success, string Message, CourseDetailDto Data)> GetCourseDetailsAsync(int courseId);
        Task<(bool Success, string Message)> UpdateCourseAsync(int courseId, UpdateCourseDto dto, string teacherId);
        Task<(bool Success, string Message)> DeleteCourseAsync(int courseId, string teacherId);
        Task<(bool Success, string Message)> EnrollStudentAsync(int courseId, string studentId);
        Task<(bool Success, string Message)> GetCoursesForAdminAsync();
    }

    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IRepository<Enrollment> _enrollmentRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IRepository<Enrollment> enrollmentRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public async Task<(bool Success, string Message, CourseDto Data)> CreateCourseAsync(CreateCourseDto dto, string teacherId)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                TeacherId = teacherId
            };

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();

            var courseDto = _mapper.Map<CourseDto>(course);
            return (true, "Course created successfully.", courseDto);
        }

        public async Task<(bool Success, string Message, IEnumerable<CourseDto> Data)> GetTeacherCoursesAsync(string teacherId)
        {
            var courses = await _courseRepository.GetCoursesByTeacherAsync(teacherId);
            var dtos = new List<CourseDto>();

            foreach (var course in courses)
            {
                var studentCount = await _courseRepository.GetStudentCountAsync(course.Id);
                var dto = _mapper.Map<CourseDto>(course);
                dto.StudentCount = studentCount;
                dtos.Add(dto);
            }

            return (true, "Courses retrieved successfully.", dtos);
        }

        public async Task<(bool Success, string Message, IEnumerable<CourseDto> Data)> GetStudentCoursesAsync(string studentId)
        {
            var courses = await _courseRepository.GetEnrolledCoursesAsync(studentId);
            var dtos = new List<CourseDto>();

            foreach (var course in courses)
            {
                var studentCount = await _courseRepository.GetStudentCountAsync(course.Id);
                var dto = _mapper.Map<CourseDto>(course);
                dto.StudentCount = studentCount;
                dtos.Add(dto);
            }

            return (true, "Enrolled courses retrieved successfully.", dtos);
        }

        public async Task<(bool Success, string Message, CourseDetailDto Data)> GetCourseDetailsAsync(int courseId)
        {
            var course = await _courseRepository.GetCourseWithDetailsAsync(courseId);
            if (course == null)
            {
                return (false, "Course not found.", null);
            }

            var studentCount = await _courseRepository.GetStudentCountAsync(courseId);
            var detailDto = _mapper.Map<CourseDetailDto>(course);
            detailDto.StudentCount = studentCount;

            return (true, "Course details retrieved successfully.", detailDto);
        }

        public async Task<(bool Success, string Message)> UpdateCourseAsync(int courseId, UpdateCourseDto dto, string teacherId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                return (false, "Course not found.");
            }

            if (course.TeacherId != teacherId)
            {
                return (false, "You are not authorized to update this course.");
            }

            course.Title = dto.Title;
            course.Description = dto.Description;
            course.UpdatedAt = DateTime.UtcNow;

            _courseRepository.Update(course);
            await _courseRepository.SaveChangesAsync();

            return (true, "Course updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteCourseAsync(int courseId, string teacherId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                return (false, "Course not found.");
            }

            if (course.TeacherId != teacherId)
            {
                return (false, "You are not authorized to delete this course.");
            }

            _courseRepository.Remove(course);
            await _courseRepository.SaveChangesAsync();

            return (true, "Course deleted successfully.");
        }

        public async Task<(bool Success, string Message)> EnrollStudentAsync(int courseId, string studentId)
        {
            var existingEnrollment = await _enrollmentRepository.FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId);
            if (existingEnrollment != null)
            {
                return (false, "Student is already enrolled in this course.");
            }

            var enrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = studentId
            };

            await _enrollmentRepository.AddAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();

            return (true, "Student enrolled successfully.");
        }

        public async Task<(bool Success, string Message, IEnumerable<CourseDto> Data)> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            var dtos = new List<CourseDto>();

            foreach (var course in courses)
            {
                var studentCount = await _courseRepository.GetStudentCountAsync(course.Id);
                var dto = _mapper.Map<CourseDto>(course);
                dto.StudentCount = studentCount;
                dtos.Add(dto);
            }

            return (true, "All courses retrieved successfully.", dtos);
        }

        public async Task<(bool Success, string Message)> GetCoursesForAdminAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return (true, "Courses retrieved successfully.");
        }
    }
}
