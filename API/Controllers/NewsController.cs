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
    public class NewsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<News> _NewsRepo;
        private readonly IMapper _mapper;
        public NewsController(IUnitOfWork unitOfWork, 
            IGenericRepository<News> NewsRepo, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _NewsRepo = NewsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<NewsDto>>> GetNews()
        {
            var spec = new NewsSpecification();

            var News = await _NewsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<NewsDto>>(News));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NewsDto>> GetNews(int id)
        {
            var spec = new NewsSpecification(id);

            var News = await _NewsRepo.GetEntityWithSpec(spec);

            if (News == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<News, NewsDto>(News);
        }

        [HttpPost]
        public async Task<ActionResult<News>> CreateNews(NewsDto NewsDto)
        {

            var News = _mapper.Map<News>(NewsDto);

            _unitOfWork.Repository<News>().Add(News);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to create News");
        }


        [HttpPut]
        public async Task<ActionResult> UpdateNews(NewsDto NewsDto)
        {
            var spec = new NewsSpecification(NewsDto.Id);

            var News = await _NewsRepo.GetEntityWithSpec(spec);

            if (News == null) return NotFound();

            _mapper.Map(NewsDto, News);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to update News");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNews(int id)
        {
            var spec = new NewsSpecification(id);

            var News = await _NewsRepo.GetEntityWithSpec(spec);

            if (News == null) return NotFound();

            // News.IsDelete = true; (for soft delete)

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to delete News");
        }
    }
}