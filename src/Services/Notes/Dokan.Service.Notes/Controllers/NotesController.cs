using Dokan.Service.Notes.Models;
using Dokan.Service.Notes.Service;
using Microsoft.AspNetCore.Mvc;

namespace Dokan.Service.Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _service;

        public NotesController(INoteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetNoteList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            var result = await _service.GetNote(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Note note)
        {
            var result = await _service.CreateNote(note);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] Note note)
        {
            var result = await _service.UpdateNote(note);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var result = await _service.DeleteNote(id);

            return Ok(result);
        }
    }
}