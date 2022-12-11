using Dokan.Service.Notes.Models;

namespace Dokan.Service.Notes.Service
{
    public class NoteService : INoteService
    {
        private readonly IDbService _dbService;

        public NoteService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<bool> CreateNote(Note note)
        {
            var result =
                await _dbService.EditData(
                    "INSERT INTO public.note (Id,Content, CreatedAt) VALUES (@Id, @Content, @CreatedAt)",
                    note);
            return true;
        }

        public async Task<List<Note>> GetNoteList()
        {
            var noteList = await _dbService.GetAll<Note>("SELECT * FROM public.note", new { });
            return noteList;
        }


        public async Task<Note> GetNote(int id)
        {
            var noteObj = await _dbService.GetAsync<Note>("SELECT * FROM public.note where Id=@id", new { id });
            return noteObj;
        }

        public async Task<Note> UpdateNote(Note note)
        {
            var updateNote =
                await _dbService.EditData(
                    "Update public.note SET Content=@Content, CreatedAt=@CreatedAt WHERE id=@Id",
                    note);
            return note;
        }

        public async Task<bool> DeleteNote(int id)
        {
            var deleteEmployee = await _dbService.EditData("DELETE FROM public.note WHERE Id=@Id", new { id });
            return true;
        }        
    }
}
