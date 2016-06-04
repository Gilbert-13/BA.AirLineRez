using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineLibrary.Passengers
{
    public interface IPassenger
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Passport { get; set; }
        DateTime Birthday { get; set; }
        int Age { get; }
        Sex Sex { get; set; }
        ITicket Ticket { get; }
        Nationality Nationality { get; set; }
    }
}
