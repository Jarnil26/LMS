using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.Backend.Data;
using LMS.Backend.Models;

namespace LMS.Backend.Repositories
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        Task<IEnumerable<Assignment>> GetCourseAssignmentsAsync(int courseId);
        Task<Assignment> GetAssignmentWithSubmissionsAsync(int assignmentId);
        Task<int> GetPendingSubmissionsCountAsync(int assignmentId);
        Task<IEnumerable<Assignment>> GetStudentAssignmentsAsync(string studentId);
    }

    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Assignment>> GetCourseAssignmentsAsync(int courseId)
        {
            return await _dbSet
                .Where(a => a.CourseId == courseId)
                .OrderByDescending(a => a.DueDate)
                .ToListAsync();
        }

        public async Task<Assignment> GetAssignmentWithSubmissionsAsync(int assignmentId)
        {
            return await _dbSet
                .Include(a => a.Course)
                .Include(a => a.Submissions)
                    .ThenInclude(s => s.Student)
                .FirstOrDefaultAsync(a => a.Id == assignmentId);
        }

        public async Task<int> GetPendingSubmissionsCountAsync(int assignmentId)
        {
            return await _context.Submissions
                .CountAsync(s => s.AssignmentId == assignmentId && s.Grade == null);
        }

        public async Task<IEnumerable<Assignment>> GetStudentAssignmentsAsync(string studentId)
        {
            var courseIds = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.CourseId)
                .ToListAsync();

            return await _dbSet
                .Include(a => a.Course)
                .Where(a => courseIds.Contains(a.CourseId))
                .OrderByDescending(a => a.DueDate)
                .ToListAsync();
        }
    }
}
