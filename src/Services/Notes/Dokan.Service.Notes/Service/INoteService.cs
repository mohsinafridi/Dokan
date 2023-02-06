using Dokan.Service.Notes.Models;

namespace Dokan.Service.Notes.Service
{
    public interface INoteService
    {
        Task<bool> CreateNote(Note note);
        Task<List<Note>> GetNoteList();
        Task<Note> UpdateNote(Note note);
        Task<bool> DeleteNote(int key);

        Task<Note> GetNote(int id);
    }
}
