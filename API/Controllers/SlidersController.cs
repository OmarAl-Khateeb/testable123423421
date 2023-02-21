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
    public class SlidersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Slider> _SlidersRepo;
        private readonly IMapper _mapper;
        public SlidersController(IUnitOfWork unitOfWork, 
            IGenericRepository<Slider> SlidersRepo, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _SlidersRepo = SlidersRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<SliderDto>>> GetSliders()
        {
            var spec = new SliderSpecification();

            var Sliders = await _SlidersRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<SliderDto>>(Sliders));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SliderDto>> GetSlider(int id)
        {
            var spec = new SliderSpecification(id);

            var Slider = await _SlidersRepo.GetEntityWithSpec(spec);

            if (Slider == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Slider, SliderDto>(Slider);
        }

        [HttpPost]
        public async Task<ActionResult<Slider>> CreateSlider(SliderDto SliderDto)
        {

            var Slider = _mapper.Map<Slider>(SliderDto);

            _unitOfWork.Repository<Slider>().Add(Slider);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to create Slider");
        }


        [HttpPut]
        public async Task<ActionResult> UpdateSlider(SliderDto SliderDto)
        {
            var spec = new SliderSpecification(SliderDto.Id);

            var Slider = await _SlidersRepo.GetEntityWithSpec(spec);

            if (Slider == null) return NotFound();

            _mapper.Map(SliderDto, Slider);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to update Slider");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSlider(int id)
        {
            var spec = new SliderSpecification(id);

            var Slider = await _SlidersRepo.GetEntityWithSpec(spec);

            if (Slider == null) return NotFound();

            // Slider.IsDelete = true; (for soft delete)

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to delete Slider");
        }
    }
}