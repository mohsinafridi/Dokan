using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Models
{
    public class Ticket
    {
        public string? UserName { get; set; }
        public DateTime BookedOn { get; set; }
        public string? Boarding { get; set; }
        public string? Destination { get; set; }
    }
}
