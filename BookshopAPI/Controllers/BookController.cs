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
            IEnumerable<AuthorDTO> authorDtos =_bookService.GetAuthors(id);
            var authors = mapper.Map<List<AuthorViewModel>>(authorDtos);
            return Ok(authors);
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin, Member")]
        [HttpGet("{id}")]
        public ActionResult<BookViewModel> Get(Guid id)
        {
            BookDTO bookDto =_bookService.Get(id);
            if (bookDto is null)
            {
                return NotFound();
            }
            var book = mapper.Map<BookViewModel>(bookDto);
            return Ok(book);
        }
        [HttpPost]
        public ActionResult<BookViewModel> Create(BookViewModel bookviewmodel)
        {
            _bookService.Create(mapper.Map<BookDTO>(bookviewmodel));
            return CreatedAtAction("Create", bookviewmodel);
        }
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _bookService.Delete(id);
            IEnumerable<BookDTO> authorDtos = _bookService.GetAll();
            GetAll();
        }
        [HttpPut]
        public ActionResult<AuthorViewModel> Update(BookViewModel bookviewmodel)
        {
            _bookService.Update(mapper.Map<BookDTO>(bookviewmodel));
            return CreatedAtAction("Create", bookviewmodel);
        }
    }
}