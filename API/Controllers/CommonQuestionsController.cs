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
    public class CommonQuestionsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<CommonQuestion> _CommonQuestionsRepo;
        private readonly IMapper _mapper;
        public CommonQuestionsController(IUnitOfWork unitOfWork, 
            IGenericRepository<CommonQuestion> CommonQuestionsRepo, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _CommonQuestionsRepo = CommonQuestionsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommonQuestionDto>>> GetCommonQuestions()
        {
            var spec = new CommonQuestionSpecification();

            var CommonQuestions = await _CommonQuestionsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<CommonQuestionDto>>(CommonQuestions));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommonQuestionDto>> GetCommonQuestion(int id)
        {
            var spec = new CommonQuestionSpecification(id);

            var CommonQuestion = await _CommonQuestionsRepo.GetEntityWithSpec(spec);

            if (CommonQuestion == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<CommonQuestion, CommonQuestionDto>(CommonQuestion);
        }

        [HttpPost]
        public async Task<ActionResult<CommonQuestion>> CreateCommonQuestion(CommonQuestionDto CommonQuestionDto)
        {

            var CommonQuestion = _mapper.Map<CommonQuestion>(CommonQuestionDto);

            _unitOfWork.Repository<CommonQuestion>().Add(CommonQuestion);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to create CommonQuestion");
        }


        [HttpPut]
        public async Task<ActionResult> UpdateCommonQuestion(CommonQuestionDto CommonQuestionDto)
        {
            var spec = new CommonQuestionSpecification(CommonQuestionDto.Id);

            var CommonQuestion = await _CommonQuestionsRepo.GetEntityWithSpec(spec);

            if (CommonQuestion == null) return NotFound();

            _mapper.Map(CommonQuestionDto, CommonQuestion);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to update CommonQuestion");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommonQuestion(int id)
        {
            var spec = new CommonQuestionSpecification(id);

            var CommonQuestion = await _CommonQuestionsRepo.GetEntityWithSpec(spec);

            if (CommonQuestion == null) return NotFound();

            // CommonQuestion.IsDelete = true; (for soft delete)

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to delete CommonQuestion");
        }
    }
}