using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineLibrary.Flights;
using AirLineLibrary.Passengers;

namespace AirLineLibrary.Helpers
{
    public static class TicketManager
    {
        public static decimal CalculateDiscount(FlightClass fc) {
            decimal discount = 1.0M;

            if (fc == FlightClass.Economy)
            {
                discount *= 0.8M;
            }

            if (DateTime.Now.Month == 1 && DateTime.Now.Day == 1)
            {
                discount *= 0.8M;
            } else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                discount *= 0.95M;
            }

            return discount;
        }

        public static decimal CalculateFullprice(FlightClass fc, decimal baseprice) {
            return CalculateDiscount(fc) * baseprice;
        }

        public static decimal DiscountInPercent(FlightClass fc) {
            decimal discount = CalculateDiscount(fc);
            return (100 * (1.0M - discount));

        }
    }
}
