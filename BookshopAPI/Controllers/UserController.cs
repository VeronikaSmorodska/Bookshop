using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BookshopBLL.DTO;
using BookshopAPI.Models;
using BookshopBLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BookshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper mapper;
        public UserController(IUserService serv, IMapper _mapper)
        {
            _userService = serv;
            mapper = _mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<UserViewModel> GetAll()
        {
            IEnumerable<UserDTO> userDtos = _userService.GetAll();
            var users = mapper.Map<IEnumerable<UserDTO>, List<UserViewModel>>(userDtos);
            return Ok(users);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<UserViewModel> Get(Guid id)
        {
            UserDTO userDto =_userService.Get(id);
            if (userDto is null)
            {
                return NotFound("Object with this id does not exist.");
            }
            var user = mapper.Map<UserViewModel>(userDto);
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpGet("{name}/names")]
        public ActionResult<UserViewModel> Get(string name)
        {
            IEnumerable<UserDTO> userDtos = _userService.GetByName(name);
            if (userDtos is null)
            {
                return NotFound();
            }
            var users = mapper.Map<IEnumerable<UserDTO>, List<UserViewModel>>(userDtos);
            return Ok(users);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<UserViewModel> Create(UserViewModel userviewmodel)
        {
            if (ModelState.IsValid)
            {
                _userService.Create(mapper.Map<UserDTO>(userviewmodel));
            }
            return CreatedAtAction("Create", userviewmodel);
        }
        //[HttpDelete("{id}")]
        public ActionResult<UserViewModel> Delete(Guid id)
        {
            UserDTO userDto = _userService.Delete(id);
            var user = mapper.Map<UserViewModel>(userDto);
            return Ok(user);
        }
       // [HttpPut]
        public ActionResult Update(UserViewModel userviewmodel)
        {
            _userService.Update(mapper.Map<UserDTO>(userviewmodel));
            return NoContent();
        }
    }
}