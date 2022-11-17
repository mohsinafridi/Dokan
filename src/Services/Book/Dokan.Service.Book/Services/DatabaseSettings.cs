using Dokan.Service.Book.Interfaces;

namespace Dokan.Service.Book.Services
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string? ConnectionString { get; set ; }
        public string? DatabaseName { get; set; }
    }
}
