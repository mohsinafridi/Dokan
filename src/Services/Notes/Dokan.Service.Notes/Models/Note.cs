namespace Dokan.Service.Notes.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string? Content {get; set; }

        public DateTime CreatedAt{ get; set; }
    }
}
