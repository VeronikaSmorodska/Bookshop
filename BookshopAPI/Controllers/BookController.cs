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

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper mapper;
        public BookController(IBookService serv, IMapper _mapper)
        {
            _bookService = serv;
            mapper = _mapper;
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin, Member")]
        [HttpGet]
        public ActionResult<BookViewModel> GetAll()
        {
            IEnumerable<BookDTO> bookDtos = _bookService.GetAll();
            var books = mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(bookDtos);
            return Ok(books);
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin, Member")]
        [HttpGet("{id}/authors")]
        public ActionResult<AuthorViewModel> GetAuthors(Guid id)
        {
            IEnumerable<AuthorDTO> authorDtos = _bookService.GetAuthors(id);
            var authors = mapper.Map<List<AuthorViewModel>>(authorDtos);
            return Ok(authors);
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin, Member")]
        [HttpGet("{id}")]
        public ActionResult<BookViewModel> Get(Guid id)
        {
            BookDTO bookDto = _bookService.Get(id);
            if (bookDto is null)
            {
                return NotFound("Object with this id does not exist.");
            }
            var book = mapper.Map<BookViewModel>(bookDto);
            return Ok(book);
        }
        [AllowAnonymous]
        [HttpGet("{title}/titles")]
        public ActionResult<BookViewModel> Get(string title)
        {
            IEnumerable<BookDTO> bookDtos = _bookService.GetByTitle(title);
            if (bookDtos is null)
            {
                return NotFound();
            }
            var books = mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(bookDtos);
            return Ok(books);
        }
        [HttpPost]
        public ActionResult<BookViewModel> Create(BookViewModel bookviewmodel)
        {
            _bookService.Create(mapper.Map<BookDTO>(bookviewmodel));
            return CreatedAtAction("Create", bookviewmodel);
        }
        [HttpDelete("{id}")]
        public ActionResult<BookViewModel> Delete(Guid id)
        {
            BookDTO bookDto = _bookService.Delete(id);
            var book = mapper.Map<BookViewModel>(bookDto);
            return Ok(book);
        }
        [HttpPut]
        public ActionResult Update(BookViewModel bookviewmodel)
        {
            _bookService.Update(mapper.Map<BookDTO>(bookviewmodel));
            return NoContent();
        }
    }
}