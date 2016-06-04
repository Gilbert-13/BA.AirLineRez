using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineLibrary.Flights
{
    public enum FlightStatus
    {
        Checkin,
        Gateclosed,
        Arrived,
        Departuredat,
        Unknown,
        Canceled,
        Expectedat,
        Delayed,
        Inflight
    }
}
