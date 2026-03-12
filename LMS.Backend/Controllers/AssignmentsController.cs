using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using LMS.Backend.DTOs;
using LMS.Backend.Services;

namespace LMS.Backend.Controllers
{
    [ApiController]
    [Route("api/courses/{courseId}/assignments")]
    [Authorize]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateAssignment(int courseId, [FromBody] CreateAssignmentDto dto)
        {
            var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message, data) = await _assignmentService.CreateAssignmentAsync(courseId, dto, teacherId);

            if (!success)
                return BadRequest(new { success = false, message });

            return CreatedAtAction(nameof(GetAssignmentDetails), new { courseId, id = data.Id }, new { success = true, message, data });
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseAssignments(int courseId)
        {
            var (success, message, data) = await _assignmentService.GetCourseAssignmentsAsync(courseId);
            return Ok(new { success, message, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentDetails(int courseId, int id)
        {
            var (success, message, data) = await _assignmentService.GetAssignmentDetailsAsync(id);

            if (!success)
                return NotFound(new { success = false, message });

            return Ok(new { success, message, data });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateAssignment(int courseId, int id, [FromBody] CreateAssignmentDto dto)
        {
            var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message) = await _assignmentService.UpdateAssignmentAsync(id, dto, teacherId);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteAssignment(int courseId, int id)
        {
            var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message) = await _assignmentService.DeleteAssignmentAsync(id, teacherId);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
        }

        [HttpGet("/api/assignments/my-assignments")]
        [Authorize(Roles = "Student,Teacher")]
        public async Task<IActionResult> GetMyAssignments()
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message, data) = await _assignmentService.GetStudentAssignmentsAsync(studentId);
            return Ok(new { success, message, data });
        }
    }
}
