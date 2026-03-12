using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using LMS.Backend.DTOs;
using LMS.Backend.Services;

namespace LMS.Backend.Controllers
{
    [ApiController]
    [Route("api/assignments/{assignmentId}/submissions")]
    [Authorize]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionsController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> SubmitAssignment(int assignmentId, [FromBody] CreateSubmissionDto dto)
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message, data) = await _submissionService.SubmitAssignmentAsync(assignmentId, studentId, dto);

            if (!success)
                return BadRequest(new { success = false, message });

            return CreatedAtAction(nameof(GetSubmission), new { assignmentId, id = data.Id }, new { success = true, message, data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignmentSubmissions(int assignmentId)
        {
            var (success, message, data) = await _submissionService.GetAssignmentSubmissionsAsync(assignmentId);
            return Ok(new { success, message, data });
        }

        [HttpGet("/api/submissions/my-submissions")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMySubmissions()
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message, data) = await _submissionService.GetStudentSubmissionsAsync(studentId);
            return Ok(new { success, message, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubmission(int assignmentId, int id)
        {
            var (success, message, data) = await _submissionService.GetSubmissionAsync(id);

            if (!success)
                return NotFound(new { success = false, message });

            return Ok(new { success, message, data });
        }

        [HttpPut("{id}/grade")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GradeSubmission(int assignmentId, int id, [FromBody] GradeSubmissionDto dto)
        {
            var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message) = await _submissionService.GradeSubmissionAsync(id, dto, teacherId);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
        }
    }
}
