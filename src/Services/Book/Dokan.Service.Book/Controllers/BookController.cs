using Dokan.Service.Book.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dokan.Service.Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {             
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookService.GetBooks());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_bookService.Find(id));
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Models.Book book)
        {
            _bookService.AddBook(book);
            return Ok(book);
        }

        [HttpPut]
        public IActionResult UpdateBook(Models.Book book)
        {
            _bookService.Update(book);
            return Ok(book);
        }

        [HttpDelete]
        public IActionResult DeleteBook([FromQuery]string id)
        {
            _bookService.Delete(id);
            return Ok();
        }
    }
}