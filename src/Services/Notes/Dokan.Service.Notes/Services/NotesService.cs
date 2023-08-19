using Dokan.Service.Notes.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace Dokan.Service.Notes.Services
{
    public class NotesService
    {
        private readonly IMongoCollection<Dokan.Service.Notes.Models.Notes> _noteCollection;
        public NotesService(IOptions<NoteDatabaseSetting> noteStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(noteStoreDatabaseSettings.Value.NotesConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(noteStoreDatabaseSettings.Value.DatabaseName);
            _noteCollection = mongoDatabase.GetCollection<Models.Notes>(noteStoreDatabaseSettings.Value.NotesCollectionName);
        }

        public async Task<List<Models.Notes>> GetAsync() =>
            await _noteCollection.Find(_ => true).ToListAsync();

        public async Task<Models.Notes?> GetAsync(string id) =>
            await _noteCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Models.Notes newNote) =>
            await _noteCollection.InsertOneAsync(newNote);

        public async Task UpdateAsync(string id, Models.Notes updatedNote) =>
            await _noteCollection.ReplaceOneAsync(x => x.Id == id, updatedNote);

        public async Task RemoveAsync(string id) =>
            await _noteCollection.DeleteOneAsync(x => x.Id == id);
    }
}
