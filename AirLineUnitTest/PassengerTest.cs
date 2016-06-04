using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirLineLibrary.Passengers;
using AirLineLibrary.Helpers;


namespace AirLineUnitTest
{
    [TestClass]
    public class PassengerTest
    {
        [TestMethod]
        public void ComparePassenger() {
            //arrange
            
            string firstname = "Name";
            string lastname = "Lastname";
            string passport = "Passport";
            DateTime birthday = DateTime.Now;
            Sex sex = Sex.Female;
            Nationality national = Nationality.American;
            FlightClass fc = FlightClass.Business;
            Ticket ticket = new Ticket(fc, 400M);

            Passenger pass1 = new Passenger(firstname, lastname, passport, birthday, sex, national, ticket);
            Passenger pass2 = new Passenger(firstname, lastname, passport, birthday, sex, national, new Ticket(FlightClass.Business, 100M));
            Passenger pass3 = new Passenger(firstname, lastname, passport, birthday, sex, national, ticket);
            
            //assert
            Assert.AreNotEqual(pass1, pass2);
            Assert.AreEqual(pass1, pass3);

            /* Comparison between passengers is made through ticket
             * If tickets are not equal than passengers also            
             */
        }

        [TestMethod]
        public void AgeOfPassenger() {
            //arrange
            DateTime birthday = new DateTime(1970, 2, 20, 0, 0, 0);
            string firstname = "Name";
            string lastname = "Lastname";
            string passport = "Passport";            
            Sex sex = Sex.Female;
            Nationality national = Nationality.American;
            FlightClass fc = FlightClass.Business;
            Ticket ticket = new Ticket(fc, 400M);
            Passenger pass1 = new Passenger(firstname, lastname, passport, birthday, sex, national, ticket);

            //act
            int result = pass1.Age;
            int expected = 46;

            //assert
            Assert.AreEqual(result, expected);
            

            /*Calculates age of the passenger */

        }
    }
}
