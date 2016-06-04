using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirLineLibrary.Passengers;

namespace AirLineUnitTest
{
    [TestClass]
    public class TicketTest
    {
        [TestMethod]
        public void CompareTickets() {
            //arrange
            FlightClass fc = FlightClass.Business;
            Ticket ticket1 = new Ticket(fc, 400M);
            Ticket ticket2 = new Ticket(fc, 400M);
            //act
            //Should return false
            bool result = ticket1 == ticket2;
            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckDiscountForTicket() {
            //arrange
            FlightClass business = FlightClass.Business;
            FlightClass econom = FlightClass.Economy;

            Ticket ticket1 = new Ticket(business, 400M);
            Ticket ticket2 = new Ticket(business, 400M);
            Ticket ticket3 = new Ticket(econom, 400M);

            //act
            bool fullpriceT1andT2 = ticket1.FullPrice == ticket2.FullPrice;
            bool fullpriceT1andT3 = ticket1.FullPrice == ticket3.FullPrice;
            decimal coef = (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                ? 0.95M : 1M;
            coef *= (DateTime.Now.Month == 1 && DateTime.Now.Day == 1) ? 0.8M : 1M;
            decimal expectedFullpriceT3 = 0.8M * coef * 400M;
            decimal resultFullpriceT3 = ticket3.FullPrice;

            //Assert
            Assert.IsTrue(fullpriceT1andT2);
            Assert.IsFalse(fullpriceT1andT3);
            Assert.AreEqual(Math.Round(expectedFullpriceT3, 2), Math.Round(resultFullpriceT3, 2));

        }
    }
}
