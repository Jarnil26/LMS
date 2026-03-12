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
    public interface ILectureMaterialService
    {
        Task<(bool Success, string Message, LectureMaterialDto Data)> UploadMaterialAsync(int courseId, CreateLectureMaterialDto dto, string fileUrl, string teacherId);
        Task<(bool Success, string Message, IEnumerable<LectureMaterialDto> Data)> GetCourseMaterialsAsync(int courseId);
        Task<(bool Success, string Message)> DeleteMaterialAsync(int materialId, string teacherId);
    }

    public class LectureMaterialService : ILectureMaterialService
    {
        private readonly IRepository<LectureMaterial> _materialRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public LectureMaterialService(IRepository<LectureMaterial> materialRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<(bool Success, string Message, LectureMaterialDto Data)> UploadMaterialAsync(int courseId, CreateLectureMaterialDto dto, string fileUrl, string teacherId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                return (false, "Course not found.", null);
            }

            if (course.TeacherId != teacherId)
            {
                return (false, "You are not authorized to upload materials for this course.", null);
            }

            var material = new LectureMaterial
            {
                CourseId = courseId,
                Title = dto.Title,
                Description = dto.Description,
                FileType = dto.FileType,
                FileUrl = fileUrl
            };

            await _materialRepository.AddAsync(material);
            await _materialRepository.SaveChangesAsync();

            var materialDto = _mapper.Map<LectureMaterialDto>(material);
            return (true, "Material uploaded successfully.", materialDto);
        }

        public async Task<(bool Success, string Message, IEnumerable<LectureMaterialDto> Data)> GetCourseMaterialsAsync(int courseId)
        {
            var materials = await _materialRepository.FindAsync(m => m.CourseId == courseId);
            var dtos = _mapper.Map<IEnumerable<LectureMaterialDto>>(materials);

            return (true, "Materials retrieved successfully.", dtos);
        }

        public async Task<(bool Success, string Message)> DeleteMaterialAsync(int materialId, string teacherId)
        {
            var material = await _materialRepository.GetByIdAsync(materialId);
            if (material == null)
            {
                return (false, "Material not found.");
            }

            var course = await _courseRepository.GetByIdAsync(material.CourseId);
            if (course.TeacherId != teacherId)
            {
                return (false, "You are not authorized to delete this material.");
            }

            _materialRepository.Remove(material);
            await _materialRepository.SaveChangesAsync();

            return (true, "Material deleted successfully.");
        }
    }
}
