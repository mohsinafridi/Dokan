using Dokan.Service.Notes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dokan.Service.Notes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesService _notesService;
        public NotesController(NotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpGet]
        public async Task<List<Models.Notes>> Get() => await _notesService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Models.Notes>> Get(string id)
        {
            var note = await _notesService.GetAsync(id);

            if (note is null)
            {
                return NotFound();
            }

            return note;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.Notes newNote)
        {
            await _notesService.CreateAsync(newNote);

            return CreatedAtAction(nameof(Get), new { id = newNote.Id }, newNote);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Models.Notes updatedBook)
        {
            var note = await _notesService.GetAsync(id);

            if (note is null)
            {
                return NotFound();
            }

            updatedBook.Id = note.Id;

            await _notesService.UpdateAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _notesService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _notesService.RemoveAsync(id);

            return NoContent();
        }

    }
}
