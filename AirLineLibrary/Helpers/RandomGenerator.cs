using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineLibrary.Flights;
using AirLineLibrary.Passengers;

namespace AirLineLibrary.Helpers
{
    public static class RandomGenerator
    {
        static DateTime generateRandomDate(int year, Random rand) {
            int month = rand.Next(1, 13);
            int[] month31 = { 1, 3, 5, 7, 8, 10, 12 };
            int day = (Array.IndexOf<int>(month31, month) != -1) ? rand.Next(1, 32) : rand.Next(1, 31);
            if (month == 2)
            {
                day = (year % 400 == 0 || year % 100 == 0 || year % 4 == 0) ? rand.Next(1, 30) : rand.Next(1, 29);
            }
            int hour = rand.Next(1, 24);
            int min = rand.Next(1, 60);
            return new DateTime(year, month, day, hour, min, 0);
        }

        public static Passenger GenerateRandomPassenger(Random rand) {
            // Some arrays for gen random passenger
            string[] arrOfMaleNames = new string[] { "Petro", "Alexsey", "Arkadiy", "Alex", "Andrey", "Anton", "Dmitro" };
            string[] arrOfFemaleName = new string[] { "Alex", "Darya", "Olena", "Oxsana", "Katerina", "Ganna", "Anastasia" };
            string[] arrOfSurnames = new string[] {"Bondarenko", "Sidorenko", "Petrenko", "Smith", "Green", "Brown", "Litvinenko", "Cavill",
            "Affleck", "Gadot", "Adams", "Eisenberg","Irons", "Lane", "Fishburne"};


            string lastname = arrOfSurnames[rand.Next(0, arrOfSurnames.Length)];

            Sex sex = (rand.NextDouble() < 0.5) ? Sex.Female : Sex.Male;

            string firstname = (sex == Sex.Female) ? arrOfFemaleName[rand.Next(arrOfFemaleName.Length)]
                : arrOfMaleNames[rand.Next(arrOfMaleNames.Length)];

            FlightClass fc = (rand.NextDouble() < 0.5) ? FlightClass.Business : FlightClass.Economy;

            Ticket ticket = new Ticket(fc, 300M);

            var values = Enum.GetValues(typeof(Nationality));
            Nationality national = (Nationality)values.GetValue(rand.Next(values.Length));

            //Generating random year within spec. range
            int year = 1950 + rand.Next(0, 51);
            DateTime birthday = generateRandomDate(year, rand);
            string passport = generateRandomPassport(rand);

            return new Passenger(firstname, lastname, passport, birthday, sex, national, ticket);

        }

        static string generateRandomPassport(Random rand) {
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char firstChar = alpha[rand.Next(0, alpha.Length - 1)];
            char secondChar = alpha[rand.Next(0, alpha.Length - 1)];
            int num = rand.Next(100000, 999999);
            return string.Format("{0}{1}{2}", firstChar, secondChar, num);
        }

        public static Flight GenerateRandomFlight(Random rand, int numOfFlight, ushort placesAvailiabe) {
            numOfFlight += 100;
            int terminal = rand.Next(1, 4);
            int gate = rand.Next(1, 11);
            ushort placesOnboard = placesAvailiabe;
            DateTime dateAndTime = generateRandomDate(2016, rand);
            var valuesOfFlightStatus = Enum.GetValues(typeof(FlightStatus));
            FlightStatus fs = (FlightStatus)valuesOfFlightStatus.GetValue(rand.Next(valuesOfFlightStatus.Length));

            FlightType ft = (rand.NextDouble() < 0.5) ? FlightType.Arrival : FlightType.Departure;

            string[] cities = { "Kiev", "Kharkiv", "London", "Paris", "Berlin", "Madrid" };
            string city = cities[rand.Next(cities.Length)];

            decimal basePrice = rand.Next(100, 500);

            return new Flight(numOfFlight, placesOnboard, fs, ft, dateAndTime,
                city, terminal, gate, basePrice);
        }

        public static Flight GenerateRandomFlight(Random rand, int numOfFlight) {
            numOfFlight += 100;
            int terminal = rand.Next(1, 4);
            int gate = rand.Next(1, 11);
            ushort placesOnboard = (ushort)rand.Next(10, 800);
            DateTime dateAndTime = generateRandomDate(2016, rand);
            var valuesOfFlightStatus = Enum.GetValues(typeof(FlightStatus));
            FlightStatus fs = (FlightStatus)valuesOfFlightStatus.GetValue(rand.Next(valuesOfFlightStatus.Length));

            FlightType ft = (rand.NextDouble() < 0.5) ? FlightType.Arrival : FlightType.Departure;

            string[] cities = { "Kiev", "Kharkiv", "London", "Paris", "Berlin", "Madrid" };
            string city = cities[rand.Next(cities.Length)];

            decimal basePrice = rand.Next(100, 500);

            return new Flight(numOfFlight, placesOnboard, fs, ft, dateAndTime, city,
                terminal, gate, basePrice);
        }

        public static Flight GenerateRandomFlightWithPassangers(Random rand, int numOfFlight,
            int numOfPassengers, ushort placesAvailiabe) {
            Flight flight = GenerateRandomFlight(rand, numOfFlight, placesAvailiabe);
            int quantity = Math.Min(flight.SeatsOnBoard, numOfPassengers);
            for (int i = 0; i < quantity; i++)
            {
                flight.AddPassenger(GenerateRandomPassenger(rand));
            }
            return flight;
        }

    }
}
