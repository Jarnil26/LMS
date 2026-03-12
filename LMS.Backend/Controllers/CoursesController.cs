using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using LMS.Backend.DTOs;
using LMS.Backend.Services;

namespace LMS.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
        {
            var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message, data) = await _courseService.CreateCourseAsync(dto, teacherId);

            if (!success)
                return BadRequest(new { success = false, message });

            return CreatedAtAction(nameof(GetCourseDetails), new { id = data.Id }, new { success = true, message, data });
        }

        [HttpGet("my-courses")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetTeacherCourses()
        {
            var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message, data) = await _courseService.GetTeacherCoursesAsync(teacherId);

            return Ok(new { success, message, data });
        }

        [HttpGet("enrolled-courses")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetStudentCourses()
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message, data) = await _courseService.GetStudentCoursesAsync(studentId);

            return Ok(new { success, message, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseDetails(int id)
        {
            var (success, message, data) = await _courseService.GetCourseDetailsAsync(id);

            if (!success)
                return NotFound(new { success = false, message });

            return Ok(new { success, message, data });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto dto)
        {
            var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message) = await _courseService.UpdateCourseAsync(id, dto, teacherId);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message) = await _courseService.DeleteCourseAsync(id, teacherId);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
        }

        [HttpPost("{courseId}/enroll")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> EnrollStudent(int courseId)
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message) = await _courseService.EnrollStudentAsync(courseId, studentId);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCourses()
        {
            var (success, message, data) = await _courseService.GetAllCoursesAsync();
            return Ok(new { success, message, data });
        }
    }
}
