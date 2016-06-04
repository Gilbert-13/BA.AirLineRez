using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineLibrary.Flights;
using AirLineLibrary.Passengers;
using System.Globalization;

namespace AirLineLibrary.Helpers
{
    public static class UserInput
    {
        static bool isValidEnum<TEnum>(string value, out TEnum result) where TEnum : struct {
            return Enum.TryParse(value, true, out result);
        }

        public static TEnum ValidateEnum<TEnum>() where TEnum : struct {
            string listOfEnums = string.Join(", ", Enum.GetNames(typeof(TEnum)));
            Console.WriteLine("Enter valid variant from list below:\n{0}", listOfEnums);
            string s = Console.ReadLine();
            TEnum e;
            bool b = isValidEnum(s, out e);
            while (!b)
            {
                Console.Write("Wrong input! Try again: ");
                b = isValidEnum(Console.ReadLine(), out e);
                Console.WriteLine();
            }
            return e;
        }

        public delegate bool Validate<T>(string input, out T result) where T : struct;

        // I guess it's not a very good way to do this,
        // but it is better than making of 3-4 simmilar method
        public static T ValidateNum<T>(Validate<T> method) where T : struct {
            T result;
            Console.Write("Enter your value: ");
            bool b = method(Console.ReadLine(), out result);
            while (!b)
            {
                Console.Write("Wrong input! Try again: ");
                b = method(Console.ReadLine(), out result);
            }
            return result;
        }

        public static DateTime ValidateDateTime() {
            Console.Write("Set date (dd.MM.yyyy HH:mm): ");
            string dateTime = Console.ReadLine();
            DateTime dt;
            bool b = DateTime.TryParseExact(dateTime,
                        "dd.MM.yyyy HH:mm",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out dt);
            while (!b)
            {
                Console.WriteLine("Wrong input! Try again");
                Console.Write("Set date (dd.MM.yyyy HH:mm): ");
                dateTime = Console.ReadLine();
                b = DateTime.TryParseExact(dateTime,
                        "dd.MM.yyyy HH:mm",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out dt);
            }

            return dt;
        }

        public static DateTime ValidateDate() {
            Console.Write("Set date (dd.MM.yyyy): ");
            string dateTime = Console.ReadLine();
            DateTime dt;
            bool b = DateTime.TryParseExact(dateTime,
                        "dd.MM.yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out dt);
            while (!b)
            {
                Console.WriteLine("Wrong input! Try again");
                Console.Write("Set date (dd.MM.yyyy): ");
                dateTime = Console.ReadLine();
                b = DateTime.TryParseExact(dateTime,
                        "dd.MM.yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out dt);
            }

            return dt;
        }

        public static void SearchPassengerMenu(FlightManager fm) {
            string messege = string.Format("Please set your option:\n(1) Search by Firstname\n(2) Search by Lastname" +
                "\n(3) Search by Firstname or Lastname\n(4) Search by flight number\n(5) Search by passport\n(0)Exit");

            int selector;
            do
            {
                Console.WriteLine("\n" + messege);
                Console.Write("OPTION: ");
                selector = ValidateNum<int>(int.TryParse);

                switch (selector)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Enter name of a passenger: ");
                        string firstname = Console.ReadLine();
                        fm.PrintPassengersFromQueue(fm.FindPassenger(fm.IsPassengerByFirstname, firstname));
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Enter lastname of a passenger: ");
                        string lastname = Console.ReadLine();
                        fm.PrintPassengersFromQueue(fm.FindPassenger(fm.IsPassengerByLastname, lastname));
                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("Enter name of a passenger: ");
                        firstname = Console.ReadLine();
                        Console.Write("Enter lastname of a passenger: ");
                        lastname = Console.ReadLine();
                        fm.PrintPassengersFromQueue(fm.FindPassengerByFirstOrLastNames(fm.IsPassengerByFirstname,
                            fm.IsPassengerByLastname, firstname, lastname));
                        break;
                    case 4:
                        Console.Clear();
                        fm.PrintAllFlights();
                        fm.UserSelectFlightById().PrintPassengers();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        Console.Write("Enter passport of a passenger: ");
                        string passport = Console.ReadLine();
                        fm.PrintPassengersFromQueue(fm.FindPassenger(fm.IsPassengerByPassport, passport));
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        Console.ReadLine();
                        break;
                }

            } while (selector != 0);
        }

        public static void CRUDmenu(FlightManager fm) {
            string messege = string.Format("Please set your option:\n(1) Add flight\n(2) Edit flight" +
                "\n(3) Delete flight\n(4) Add passenger\n(5) Edit passenger\n(6) Delete passenger\n(0)Exit");

            int selector;
            do
            {
                Console.WriteLine("\n" + messege);
                Console.Write("OPTION: ");
                selector = ValidateNum<int>(int.TryParse);

                switch (selector)
                {
                    case 1:
                        Console.Clear();
                        fm.PrintAllFlights();
                        fm.AddUserInputFlight();
                        Console.Clear();
                        fm.PrintAllFlights();
                        break;
                    case 2:
                        Console.Clear();

                        fm.EditFlightById();
                        Console.Clear();
                        fm.PrintAllFlights();
                        break;
                    case 3:
                        Console.Clear();
                        fm.PrintAllFlights();
                        fm.DeleteFlightById();
                        Console.Clear();
                        fm.PrintAllFlights();
                        break;
                    case 4:
                        Console.Clear();
                        fm.PrintAllFlights();
                        Flight flight = fm.UserSelectFlightById();
                        Console.Clear();
                        flight.PrintPassengers();
                        Console.WriteLine();
                        flight.AddUserInputPassenger();
                        Console.Clear();
                        flight.PrintPassengers();
                        break;
                    case 5:
                        Console.Clear();
                        fm.PrintAllFlights();
                        Flight flight1 = fm.UserSelectFlightById();
                        Console.Clear();
                        flight1.PrintPassengers();
                        Console.WriteLine();
                        flight1.EditUserInputPassenger();
                        Console.Clear();
                        flight1.PrintPassengers();
                        break;
                    case 6:
                        Console.Clear();
                        fm.PrintAllFlights();
                        Flight flight2 = fm.UserSelectFlightById();
                        Console.Clear();
                        flight2.PrintPassengers();
                        Console.WriteLine();
                        flight2.DeleteUserInputPassenger();
                        Console.Clear();
                        flight2.PrintPassengers();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        Console.ReadLine();
                        break;
                }
            } while (selector != 0);
        }

        public static void BonusMenu(FlightManager fm) {
            string messege = string.Format("Please set your option:\n(1) Average age on flight\n(2) Total amount of money on flight" +
               "\n(0)Exit");

            int selector;
            do
            {
                Console.WriteLine("\n" + messege);
                Console.Write("OPTION: ");
                selector = ValidateNum<int>(int.TryParse);

                switch (selector)
                {
                    case 1:
                        Console.Clear();
                        fm.PrintNotEmptyFlightsAverageAge();
                        break;
                    case 2:
                        Console.Clear();
                        fm.PrintNotEmptyFlightsTotalAmountMoney();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        Console.ReadLine();
                        break;
                }
            } while (selector != 0);
        }

        public static void FlightManagerMenu(FlightManager fm) {

            string messege = string.Format("Please set your option:\n(1) Print (view) all flights (without passengers)\n" +
                "(2) Print (view) all flight’s passengers (search by flight number)" +
                "\n(3) Search passenger (inner menu)" +
                "\n(4) Search all flights (without passengers) with the price of ticket type lower than %user input% dollars" +
                "\n(5) Input, deleting and editing  of flights and passengers (inner menu)" +
                "\n(6) Bonus features" +
                "\n(0)Exit");

            fm.PrintAllFlights();
            int selector;
            do
            {

                Console.WriteLine("\n" + messege);
                Console.Write("OPTION: ");
                selector = ValidateNum<int>(int.TryParse);

                switch (selector)
                {
                    case 1:
                        Console.Clear();
                        fm.PrintEmptyFlights();
                        break;
                    case 2:
                        Console.Clear();
                        fm.PrintAllFlights();
                        Flight f = fm.UserSelectFlightById();
                        Console.Clear();
                        f.PrintPassengers();
                        Console.WriteLine();
                        f.PrintPassengersTicketInfo();
                        break;
                    case 3:
                        Console.Clear();
                        SearchPassengerMenu(fm);
                        break;
                    case 4:
                        Console.Clear();
                        Console.Write("Enter flight class: ");
                        FlightClass fc = ValidateEnum<FlightClass>();
                        Console.Write("Enter  ticket price: ");
                        decimal price = ValidateNum<decimal>(decimal.TryParse);
                        fm.PrintFlightsFromQueue(fm.FindFlightsByPriceAndClass(fc, price), fc);
                        break;
                    case 5:
                        Console.Clear();
                        CRUDmenu(fm);
                        break;
                    case 6:
                        Console.Clear();
                        BonusMenu(fm);
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Press enter to exit");
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        Console.ReadLine();
                        break;
                }

            } while (selector != 0);

        }
    }
}
