using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.Backend.Data;
using LMS.Backend.Models;

namespace LMS.Backend.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetCoursesByTeacherAsync(string teacherId);
        Task<IEnumerable<Course>> GetEnrolledCoursesAsync(string studentId);
        Task<Course> GetCourseWithDetailsAsync(int courseId);
        Task<int> GetStudentCountAsync(int courseId);
    }

    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetCoursesByTeacherAsync(string teacherId)
        {
            return await _dbSet
                .Where(c => c.TeacherId == teacherId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetEnrolledCoursesAsync(string studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.Course)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Course> GetCourseWithDetailsAsync(int courseId)
        {
            return await _dbSet
                .Include(c => c.Teacher)
                .Include(c => c.Enrollments)
                .Include(c => c.LectureMaterials)
                .Include(c => c.Assignments)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<int> GetStudentCountAsync(int courseId)
        {
            return await _context.Enrollments
                .CountAsync(e => e.CourseId == courseId);
        }
    }
}
