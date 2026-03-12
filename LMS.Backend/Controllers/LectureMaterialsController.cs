using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using LMS.Backend.DTOs;
using LMS.Backend.Services;

namespace LMS.Backend.Controllers
{
    [ApiController]
    [Route("api/courses/{courseId}/materials")]
    [Authorize]
    public class LectureMaterialsController : ControllerBase
    {
        private readonly ILectureMaterialService _materialService;

        public LectureMaterialsController(ILectureMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UploadMaterial(int courseId, [FromBody] CreateLectureMaterialDto dto, [FromQuery] string fileUrl)
        {
            var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message, data) = await _materialService.UploadMaterialAsync(courseId, dto, fileUrl, teacherId);

            if (!success)
                return BadRequest(new { success = false, message });

            return CreatedAtAction(nameof(GetCourseMaterials), new { courseId }, new { success = true, message, data });
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseMaterials(int courseId)
        {
            var (success, message, data) = await _materialService.GetCourseMaterialsAsync(courseId);
            return Ok(new { success, message, data });
        }

        [HttpDelete("{materialId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteMaterial(int courseId, int materialId)
        {
            var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (success, message) = await _materialService.DeleteMaterialAsync(materialId, teacherId);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
        }
    }
}
