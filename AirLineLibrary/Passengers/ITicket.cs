using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineLibrary.Passengers
{
    public interface ITicket
    {
        string Id { get; set; }
        FlightClass FlightClass { get; set; }
        decimal BasePrice { get; set; }

        decimal FullPrice { get; }
        decimal Discount { get; }

        string Info();
    }
}
