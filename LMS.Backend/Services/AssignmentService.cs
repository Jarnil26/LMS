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
    public interface IAssignmentService
    {
        Task<(bool Success, string Message, AssignmentDto Data)> CreateAssignmentAsync(int courseId, CreateAssignmentDto dto, string teacherId);
        Task<(bool Success, string Message, IEnumerable<AssignmentDto> Data)> GetCourseAssignmentsAsync(int courseId);
        Task<(bool Success, string Message, AssignmentDetailDto Data)> GetAssignmentDetailsAsync(int assignmentId);
        Task<(bool Success, string Message)> UpdateAssignmentAsync(int assignmentId, CreateAssignmentDto dto, string teacherId);
        Task<(bool Success, string Message)> DeleteAssignmentAsync(int assignmentId, string teacherId);
        Task<(bool Success, string Message, IEnumerable<AssignmentDto> Data)> GetStudentAssignmentsAsync(string studentId);
    }

    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public AssignmentService(IAssignmentRepository assignmentRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<(bool Success, string Message, AssignmentDto Data)> CreateAssignmentAsync(int courseId, CreateAssignmentDto dto, string teacherId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                return (false, "Course not found.", null);
            }

            if (course.TeacherId != teacherId)
            {
                return (false, "You are not authorized to create assignments for this course.", null);
            }

            var assignment = new Assignment
            {
                CourseId = courseId,
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate
            };

            await _assignmentRepository.AddAsync(assignment);
            await _assignmentRepository.SaveChangesAsync();

            var assignmentDto = _mapper.Map<AssignmentDto>(assignment);
            return (true, "Assignment created successfully.", assignmentDto);
        }

        public async Task<(bool Success, string Message, IEnumerable<AssignmentDto> Data)> GetCourseAssignmentsAsync(int courseId)
        {
            var assignments = await _assignmentRepository.GetCourseAssignmentsAsync(courseId);
            var dtos = _mapper.Map<IEnumerable<AssignmentDto>>(assignments);

            return (true, "Assignments retrieved successfully.", dtos);
        }

        public async Task<(bool Success, string Message, AssignmentDetailDto Data)> GetAssignmentDetailsAsync(int assignmentId)
        {
            var assignment = await _assignmentRepository.GetAssignmentWithSubmissionsAsync(assignmentId);
            if (assignment == null)
            {
                return (false, "Assignment not found.", null);
            }

            var detailDto = _mapper.Map<AssignmentDetailDto>(assignment);
            return (true, "Assignment details retrieved successfully.", detailDto);
        }

        public async Task<(bool Success, string Message)> UpdateAssignmentAsync(int assignmentId, CreateAssignmentDto dto, string teacherId)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return (false, "Assignment not found.");
            }

            var course = await _courseRepository.GetByIdAsync(assignment.CourseId);
            if (course.TeacherId != teacherId)
            {
                return (false, "You are not authorized to update this assignment.");
            }

            assignment.Title = dto.Title;
            assignment.Description = dto.Description;
            assignment.DueDate = dto.DueDate;
            assignment.UpdatedAt = DateTime.UtcNow;

            _assignmentRepository.Update(assignment);
            await _assignmentRepository.SaveChangesAsync();

            return (true, "Assignment updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteAssignmentAsync(int assignmentId, string teacherId)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return (false, "Assignment not found.");
            }

            var course = await _courseRepository.GetByIdAsync(assignment.CourseId);
            if (course.TeacherId != teacherId)
            {
                return (false, "You are not authorized to delete this assignment.");
            }

            _assignmentRepository.Remove(assignment);
            await _assignmentRepository.SaveChangesAsync();

            return (true, "Assignment deleted successfully.");
        }
        public async Task<(bool Success, string Message, IEnumerable<AssignmentDto> Data)> GetStudentAssignmentsAsync(string studentId)
        {
            var assignments = await _assignmentRepository.GetStudentAssignmentsAsync(studentId);
            var dtos = _mapper.Map<IEnumerable<AssignmentDto>>(assignments);

            return (true, "Assignments retrieved successfully.", dtos);
        }
    }
}
