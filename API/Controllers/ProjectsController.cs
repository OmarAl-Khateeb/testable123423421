using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructue.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProjectsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Project> _ProjectsRepo;
        private readonly IMapper _mapper;
        public ProjectsController(IUnitOfWork unitOfWork, 
            IGenericRepository<Project> ProjectsRepo, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ProjectsRepo = ProjectsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetProjects()
        {
            var spec = new ProjectSpecification();

            var Projects = await _ProjectsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<ProjectDto>>(Projects));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDto>> GetProject(int id)
        {
            var spec = new ProjectSpecification(id);

            var Project = await _ProjectsRepo.GetEntityWithSpec(spec);

            if (Project == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Project, ProjectDto>(Project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(ProjectDto ProjectDto)
        {

            var Project = _mapper.Map<Project>(ProjectDto);

            _unitOfWork.Repository<Project>().Add(Project);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to create Project");
        }


        [HttpPut]
        public async Task<ActionResult> UpdateProject(ProjectDto ProjectDto)
        {
            var spec = new ProjectSpecification(ProjectDto.Id);

            var Project = await _ProjectsRepo.GetEntityWithSpec(spec);

            if (Project == null) return NotFound();

            _mapper.Map(ProjectDto, Project);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to update Project");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var spec = new ProjectSpecification(id);

            var Project = await _ProjectsRepo.GetEntityWithSpec(spec);

            if (Project == null) return NotFound();

            // Project.IsDelete = true; (for soft delete)

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to delete Project");
        }
    }
}