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
    public class BranchesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Branch> _branchesRepo;
        private readonly IMapper _mapper;
        public BranchesController(IUnitOfWork unitOfWork, 
            IGenericRepository<Branch> branchesRepo, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _branchesRepo = branchesRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<BranchToReturnDto>>> GetBranchs()
        {
            var spec = new BranchesWithTypesSpecification();

            var branches = await _branchesRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<BranchToReturnDto>>(branches));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BranchToReturnDto>> GetBranch(int id)
        {
            var spec = new BranchesWithTypesSpecification(id);

            var branch = await _branchesRepo.GetEntityWithSpec(spec);

            if (branch == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Branch, BranchToReturnDto>(branch);
        }

        [HttpPost]
        public async Task<ActionResult<Branch>> CreateBranch(BranchUpdateDto branchUpdateDto)
        {

            var branch = _mapper.Map<Branch>(branchUpdateDto);

            _unitOfWork.Repository<Branch>().Add(branch);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to create Branch");
        }


        [HttpPut]
        public async Task<ActionResult> UpdateBranch(BranchUpdateDto branchUpdateDto)
        {
            var spec = new BranchesWithTypesSpecification(branchUpdateDto.Id);

            var branch = await _branchesRepo.GetEntityWithSpec(spec);

            if (branch == null) return NotFound();

            _mapper.Map(branchUpdateDto, branch);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to update Branch");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBranch(int id)
        {
            var spec = new BranchesWithTypesSpecification(id);

            var branch = await _branchesRepo.GetEntityWithSpec(spec);

            if (branch == null) return NotFound();

            branch.IsDelete = true;

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to delete Branch");
        }
    }
}