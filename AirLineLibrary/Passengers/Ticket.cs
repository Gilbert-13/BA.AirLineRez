using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineLibrary.Helpers;

namespace AirLineLibrary.Passengers
{
    public class Ticket : ITicket
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public FlightClass FlightClass { get; set; }

        public decimal BasePrice { get; set; }

        public decimal Discount { get { return TicketManager.DiscountInPercent(FlightClass); } }

        public decimal FullPrice { get { return TicketManager.CalculateFullprice(FlightClass, BasePrice); } }

        public Ticket(FlightClass fc, decimal basePrice) {
            FlightClass = fc;
            BasePrice = basePrice;
        }

        public string Info() {
            return string.Format($"Ticket Id: {Id} Type: {FlightClass} Price: {FullPrice}$ Discount: {100 * (1.0M - Discount)}%");
        }


    }
}
