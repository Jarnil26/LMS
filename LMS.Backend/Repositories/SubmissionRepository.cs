using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.Backend.Data;
using LMS.Backend.Models;

namespace LMS.Backend.Repositories
{
    public interface ISubmissionRepository : IRepository<Submission>
    {
        Task<Submission> GetStudentSubmissionAsync(int assignmentId, string studentId);
        Task<IEnumerable<Submission>> GetAssignmentSubmissionsAsync(int assignmentId);
        Task<IEnumerable<Submission>> GetStudentSubmissionsAsync(string studentId);
    }

    public class SubmissionRepository : Repository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Submission> GetStudentSubmissionAsync(int assignmentId, string studentId)
        {
            return await _dbSet
                .Include(s => s.Assignment)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(s => s.AssignmentId == assignmentId && s.StudentId == studentId);
        }

        public async Task<IEnumerable<Submission>> GetAssignmentSubmissionsAsync(int assignmentId)
        {
            return await _dbSet
                .Where(s => s.AssignmentId == assignmentId)
                .Include(s => s.Student)
                .Include(s => s.Assignment)
                .OrderByDescending(s => s.SubmittedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Submission>> GetStudentSubmissionsAsync(string studentId)
        {
            return await _dbSet
                .Where(s => s.StudentId == studentId)
                .Include(s => s.Assignment)
                    .ThenInclude(a => a.Course)
                .OrderByDescending(s => s.SubmittedAt)
                .ToListAsync();
        }
    }
}
