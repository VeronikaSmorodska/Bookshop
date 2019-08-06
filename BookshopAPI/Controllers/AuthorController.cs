using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BookshopBLL.DTO;
using BookshopAPI.Models;
using BookshopBLL.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;

namespace BookshopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")] 
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper mapper;
        public AuthorController(IAuthorService serv, IMapper _mapper)
        {
            _authorService = serv;
            mapper = _mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<AuthorViewModel> GetAll()
        {
            IEnumerable<AuthorDTO> authorDtos = _authorService.GetAll();
            var authors = mapper.Map<IEnumerable<AuthorDTO>, List<AuthorViewModel>>(authorDtos);
            return Ok(authors);
        }
        [AllowAnonymous]
        [HttpGet("{id}/books")]
        public ActionResult<BookViewModel> GetBooks(Guid id)
        {
            IEnumerable<BookDTO> bookDtos = _authorService.GetBooks(id);
            var books = mapper.Map<List<BookViewModel>>(bookDtos);
            return Ok(books);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<AuthorViewModel> Get(Guid id)
        {
            AuthorDTO authorDto = _authorService.Get(id);
            if(authorDto is null)
            {
                return NotFound("Object with this id does not exist.");
            }
            var author = mapper.Map<AuthorViewModel>(authorDto);
            return Ok(author);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<AuthorViewModel> Create(AuthorViewModel authorviewmodel)
        {
            _authorService.Create(mapper.Map<AuthorDTO>(authorviewmodel));
            return CreatedAtAction("Create", authorviewmodel);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<AuthorViewModel> Delete(Guid id)
        {
           AuthorDTO authorDto= _authorService.Delete(id);
            var author = mapper.Map<AuthorViewModel>(authorDto);
            return Ok(author);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Update(AuthorViewModel authorviewmodel)
        {
            _authorService.Update(mapper.Map<AuthorDTO>(authorviewmodel));
            return NoContent();
        }
    }
}