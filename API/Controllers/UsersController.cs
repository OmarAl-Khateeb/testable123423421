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
    public class UsersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _usersRepo;
        private readonly IGenericRepository<UserType> _userTypeRepo;
        private readonly IGenericRepository<UserAttachment> _userAttachmentRepo;
        private readonly IMapper _mapper;
        public UsersController(IUnitOfWork unitOfWork, 
            IGenericRepository<User> usersRepo, 
            IGenericRepository<UserType> userTypeRepo, 
            IGenericRepository<UserAttachment> userAttachmentRepo, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAttachmentRepo = userAttachmentRepo;
            _userTypeRepo = userTypeRepo;
            _usersRepo = usersRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserToReturnDto>>> GetUsers()
        {
            var spec = new UsersWithTypesSpecification();

            var users = await _usersRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<UserToReturnDto>>(users));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserToReturnDto>> GetUser(int id)
        {
            var spec = new UsersWithTypesSpecification(id);

            var user = await _usersRepo.GetEntityWithSpec(spec);

            if (user == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<User, UserToReturnDto>(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserUpdateDto userUpdateDto)
        {

            var user = _mapper.Map<User>(userUpdateDto);

            _unitOfWork.Repository<User>().Add(user);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to create user");
        }


        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var spec = new UsersWithTypesSpecification(userUpdateDto.Id);

            var user = await _usersRepo.GetEntityWithSpec(spec);

            if (user == null) return NotFound();

            _mapper.Map(userUpdateDto, user);

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to update user");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var spec = new UsersWithTypesSpecification(id);

            var user = await _usersRepo.GetEntityWithSpec(spec);

            if (user == null) return NotFound();

            user.IsDelete = true;

            if (await _unitOfWork.Complete() > 0) return NoContent();

            return BadRequest("Failed to delete user");
        }
    }
}