namespace Dokan.Service.Notes.Models
{
    public class NoteDatabaseSetting
    {
        public string NotesConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string NotesCollectionName { get; set; } = null!;
    }
}
