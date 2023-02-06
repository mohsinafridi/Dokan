using Dokan.Service.Customer.Interfaces;

namespace Dokan.Service.Customer.Services
{
    public  class DatabaseSettings : IDatabaseSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}
