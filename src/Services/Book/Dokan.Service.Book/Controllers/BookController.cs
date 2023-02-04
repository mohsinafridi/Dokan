using Dokan.Service.Book.Interfaces;
using Dokan.Service.Book.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

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
        [SwaggerResponse((int)HttpStatusCode.NoContent, "No Books Found.")]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetBooks();
            if (books.Count == 0)
            {
                return NoContent();
            }
            return Ok(await _bookService.GetBooks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var book = await _bookService.GetBookById(id);
            if (book.Id != id)
            {
                return NotFound();
            }
            return Ok(book);           
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Models.Book book)
        {
            var transactionResult = await _bookService.AddBook(book);
            switch (transactionResult)
            {
                case TransactionResult.Success:
                    return Ok();
                case TransactionResult.BadRequest:
                    return StatusCode(StatusCodes.Status400BadRequest);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(Models.Book book)
        {
            await _bookService.UpdateBook(book);
            return Ok(book);
        }

        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK, "Book Deleted.")]
        public async Task<IActionResult> DeleteBook([FromQuery]string id)
        {
            var result = await _bookService.DeleteBook(id);
             if(result)
            return Ok();

            return StatusCode(StatusCodes.Status404NotFound);
        }
    }
}