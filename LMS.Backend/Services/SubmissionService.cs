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
    public interface ISubmissionService
    {
        Task<(bool Success, string Message, SubmissionDto Data)> SubmitAssignmentAsync(int assignmentId, string studentId, CreateSubmissionDto dto);
        Task<(bool Success, string Message, IEnumerable<SubmissionDto> Data)> GetAssignmentSubmissionsAsync(int assignmentId);
        Task<(bool Success, string Message, IEnumerable<SubmissionDto> Data)> GetStudentSubmissionsAsync(string studentId);
        Task<(bool Success, string Message)> GradeSubmissionAsync(int submissionId, GradeSubmissionDto dto, string teacherId);
        Task<(bool Success, string Message, SubmissionDto Data)> GetSubmissionAsync(int submissionId);
    }

    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public SubmissionService(ISubmissionRepository submissionRepository, IAssignmentRepository assignmentRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _submissionRepository = submissionRepository;
            _assignmentRepository = assignmentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<(bool Success, string Message, SubmissionDto Data)> SubmitAssignmentAsync(int assignmentId, string studentId, CreateSubmissionDto dto)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return (false, "Assignment not found.", null);
            }

            var existingSubmission = await _submissionRepository.GetStudentSubmissionAsync(assignmentId, studentId);
            
            if (existingSubmission != null)
            {
                existingSubmission.FileUrl = dto.FileUrl;
                existingSubmission.SubmittedAt = DateTime.UtcNow;
                _submissionRepository.Update(existingSubmission);
            }
            else
            {
                var submission = new Submission
                {
                    AssignmentId = assignmentId,
                    StudentId = studentId,
                    FileUrl = dto.FileUrl
                };
                await _submissionRepository.AddAsync(submission);
            }

            await _submissionRepository.SaveChangesAsync();

            var submissionDto = _mapper.Map<SubmissionDto>(existingSubmission ?? await _submissionRepository.GetStudentSubmissionAsync(assignmentId, studentId));
            return (true, "Assignment submitted successfully.", submissionDto);
        }

        public async Task<(bool Success, string Message, IEnumerable<SubmissionDto> Data)> GetAssignmentSubmissionsAsync(int assignmentId)
        {
            var submissions = await _submissionRepository.GetAssignmentSubmissionsAsync(assignmentId);
            var dtos = _mapper.Map<IEnumerable<SubmissionDto>>(submissions);

            return (true, "Submissions retrieved successfully.", dtos);
        }

        public async Task<(bool Success, string Message, IEnumerable<SubmissionDto> Data)> GetStudentSubmissionsAsync(string studentId)
        {
            var submissions = await _submissionRepository.GetStudentSubmissionsAsync(studentId);
            var dtos = _mapper.Map<IEnumerable<SubmissionDto>>(submissions);

            return (true, "Submissions retrieved successfully.", dtos);
        }

        public async Task<(bool Success, string Message)> GradeSubmissionAsync(int submissionId, GradeSubmissionDto dto, string teacherId)
        {
            var submission = await _submissionRepository.GetByIdAsync(submissionId);
            if (submission == null)
            {
                return (false, "Submission not found.");
            }

            var assignment = await _assignmentRepository.GetByIdAsync(submission.AssignmentId);
            var course = await _courseRepository.GetByIdAsync(assignment.CourseId);

            if (course.TeacherId != teacherId)
            {
                return (false, "You are not authorized to grade this submission.");
            }

            submission.Grade = dto.Grade;
            submission.Feedback = dto.Feedback;
            submission.GradedAt = DateTime.UtcNow;

            _submissionRepository.Update(submission);
            await _submissionRepository.SaveChangesAsync();

            return (true, "Submission graded successfully.");
        }

        public async Task<(bool Success, string Message, SubmissionDto Data)> GetSubmissionAsync(int submissionId)
        {
            var submission = await _submissionRepository.GetByIdAsync(submissionId);
            if (submission == null)
            {
                return (false, "Submission not found.", null);
            }

            var dto = _mapper.Map<SubmissionDto>(submission);
            return (true, "Submission retrieved successfully.", dto);
        }
    }
}
