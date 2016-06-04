using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineLibrary.Passengers;
using AirLineLibrary.Helpers;


namespace AirLineLibrary.Flights
{
    public class FlightManager
    {
        Flight[] _flights;

        public FlightManager() {
            _flights = new Flight[10];
        }

        public FlightManager(Flight[] flighs) {
            _flights = flighs;
        }

        public bool IsFlightArrFull() {
            foreach (Flight f in _flights)
            {
                if (f == null) return false;
            }
            return true;
        }

        public void AddFlight(Flight flight) {
            int indx = _flights.Length;
            if (!IsFlightArrFull())
            {
                for (int i = 0; i < _flights.Length; i++)
                {
                    if (_flights[i] == null)
                    {
                        _flights[i] = flight;
                        Console.WriteLine("Flight was added!");
                        return;
                    }
                }
            } else
            {
                Flight[] tempFlights = new Flight[_flights.Length * 2];
                _flights.CopyTo(tempFlights, 0);
                _flights = tempFlights;
                _flights[indx] = flight;
                Console.WriteLine("Flight was added! Array was expanded!");

            }
        }

        public void AddUserInputFlight() {
            Flight flight = createEmptyFlight();
            AddFlight(flight);
        }

        public void DeleteFlightById() {
            Console.WriteLine("Enter Number of the flight: ");
            int flightNumber = UserInput.ValidateNum<int>(int.TryParse);

            for (int i = 0; i < _flights.Length; i++)
            {
                if (_flights[i] != null)
                {
                    if (_flights[i].FlightNumber == flightNumber)
                    {
                        _flights[i] = null;
                        Console.WriteLine("Flight was deleted");
                        return;
                    }
                }
            }
            Console.WriteLine("There are NO flight with #{0}. Flight was NOT deleted", flightNumber);
        }

        private void selectFlightById(int flightNum, out Flight flight) {
            flight = null;
            foreach (Flight f in _flights)
            {
                if (f != null)
                {
                    if (f.FlightNumber == flightNum)
                    {
                        flight = f;
                        return;
                    }
                }
            }

        }

        public Flight UserSelectFlightById() {
            Flight result = null;
            Console.WriteLine("Enter Number of the flight: ");
            int flightNumber = UserInput.ValidateNum<int>(int.TryParse);
            while (!isFlightExsists(flightNumber))
            {
                Console.WriteLine("Sorry there are no flight by this number! Try again!");
                Console.WriteLine("Enter Number of the flight: ");
                flightNumber = UserInput.ValidateNum<int>(int.TryParse);
            }
            selectFlightById(flightNumber, out result);
            return result;
        }

        public Flight UserSelectFlightById(out int flightNumber) {
            Flight result = null;
            Console.WriteLine("Enter Number of the flight: ");
            flightNumber = UserInput.ValidateNum<int>(int.TryParse);
            while (!isFlightExsists(flightNumber))
            {
                Console.WriteLine("Sorry there are no flight by this number! Try again!");
                Console.WriteLine("Enter Number of the flight: ");
                flightNumber = UserInput.ValidateNum<int>(int.TryParse);
            }
            selectFlightById(flightNumber, out result);
            return result;
        }

        public void EditFlightById() {
            Console.WriteLine("Edit flight");
            PrintAllFlights();
            Console.WriteLine();
            int editedFlightNum;
            Flight flight = UserSelectFlightById(out editedFlightNum);

            Console.WriteLine("Enter Number of the flight: ");
            int flightNumber = UserInput.ValidateNum<int>(int.TryParse);
            while (_flights.Where(x => x != null)
                .Count(x => x.FlightNumber == flightNumber) > 0 && flightNumber != editedFlightNum)
            {
                Console.WriteLine("Sorry flight with the same number exsists! Try again!");
                Console.WriteLine("Enter Number of the flight: ");
                flightNumber = UserInput.ValidateNum<int>(int.TryParse);
            }
            flight.FlightNumber = flightNumber;

            Console.WriteLine("Enter base price of the flight ticket, $: ");
            decimal basePriceOfTicket = UserInput.ValidateNum<decimal>(decimal.TryParse);
            while (basePriceOfTicket < 0)
            {
                Console.WriteLine("Base price of the flight ticket should be greater than 0! Try again!");
                Console.WriteLine("Enter base price of the flight ticket, $: ");
                basePriceOfTicket = UserInput.ValidateNum<decimal>(decimal.TryParse);
            }
            flight.BasePriceOfTicket = basePriceOfTicket;

            Console.WriteLine("Enter number places on board: ");
            flight.SeatsOnBoard = UserInput.ValidateNum<ushort>(ushort.TryParse);

            Console.WriteLine("Enter flight status: ");
            flight.Status = UserInput.ValidateEnum<FlightStatus>();

            Console.WriteLine("Enter flight type: ");
            flight.FlightType = UserInput.ValidateEnum<FlightType>();

            Console.WriteLine("Enter date and time: ");
            flight.DateAndTime = UserInput.ValidateDateTime();

            Console.WriteLine("Enter name of the city: ");
            flight.City = Console.ReadLine();

            Console.WriteLine("Enter terminal number: ");
            flight.Terminal = UserInput.ValidateNum<int>(int.TryParse);

            Console.WriteLine("Enter gate number: ");
            flight.Gate = UserInput.ValidateNum<int>(int.TryParse);

            Console.WriteLine("Flight was edited!");
        }

        public bool TryFlightById(int flightId, out Flight searchResult) {
            searchResult = null;
            foreach (var f in _flights)
            {
                if (flightId == f.FlightNumber)
                {
                    searchResult = f;
                    Console.WriteLine("Flight by id {0} was found", flightId);
                    return true;
                }
            }
            Console.WriteLine("Flight by id {0} was NOT found", flightId);
            return false;
        }

        private bool isFlightExsists(int flightId) {
            foreach (var f in _flights)
            {
                if (f != null)
                {
                    if (flightId == f.FlightNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Returns passengers depending from condition Firstname or Lastname or Passport
        public Queue<IPassenger> FindPassenger(Func<IPassenger, string, bool> condtion, string dataToSearct) {
            Queue<IPassenger> founded = new Queue<IPassenger>();

            foreach (Flight f in _flights)
            {
                foreach (IPassenger p in f.GetPassengers)
                {
                    if (condtion(p, dataToSearct))
                    {
                        founded.Enqueue(p);
                    }
                }
            }
            return founded;
        }

        // Returns passengers depending from 2 conditions Firstname or Lastname or Passport
        public Queue<IPassenger> FindPassengerByFirstOrLastNames(Func<IPassenger, string, bool> condtion1,
            Func<IPassenger, string, bool> condtion2,
            string firstname, string lastname) {

            Queue<IPassenger> founded = new Queue<IPassenger>();

            foreach (Flight f in _flights)
            {
                foreach (IPassenger p in f.GetPassengers)
                {
                    if (condtion1(p, firstname) || condtion2(p, lastname))
                    {
                        founded.Enqueue(p);
                    }
                }
            }
            return founded;
        }


        private Flight createEmptyFlight() {
            Console.WriteLine("Enter Number of the flight: ");
            int flightNumber = UserInput.ValidateNum<int>(int.TryParse);
            while (isFlightExsists(flightNumber))
            {
                Console.WriteLine("Sorry flight with the same number exsists! Try again!");
                Console.WriteLine("Enter Number of the flight: ");
                flightNumber = UserInput.ValidateNum<int>(int.TryParse);
            }

            Console.WriteLine("Enter base price of the flight ticket, $: ");
            decimal basePriceOfTicket = UserInput.ValidateNum<decimal>(decimal.TryParse);
            while (basePriceOfTicket < 0)
            {
                Console.WriteLine("Base price of the flight ticket should be greater than 0! Try again!");
                Console.WriteLine("Enter base price of the flight ticket, $: ");
                basePriceOfTicket = UserInput.ValidateNum<decimal>(decimal.TryParse);
            }

            Console.WriteLine("Enter number places on board: ");
            ushort placesOnBoard = UserInput.ValidateNum<ushort>(ushort.TryParse);

            Console.WriteLine("Enter flight status: ");
            FlightStatus status = UserInput.ValidateEnum<FlightStatus>();

            Console.WriteLine("Enter flight type: ");
            FlightType flightType = UserInput.ValidateEnum<FlightType>();

            Console.WriteLine("Enter date and time: ");
            DateTime dateAndTime = UserInput.ValidateDateTime();

            Console.WriteLine("Enter name of the city: ");
            string city = Console.ReadLine();

            Console.WriteLine("Enter terminal number: ");
            int terminal = UserInput.ValidateNum<int>(int.TryParse);

            Console.WriteLine("Enter gate number: ");
            int gate = UserInput.ValidateNum<int>(int.TryParse);

            return new Flight(flightNumber, placesOnBoard, status, flightType,
                dateAndTime, city, terminal, gate, basePriceOfTicket);
        }

        public bool IsPassengerByFirstname(IPassenger p, string firstname) {
            return p != null ? p.Firstname.Contains(firstname) : false;
        }

        public bool IsPassengerByLastname(IPassenger p, string lastname) {
            return p != null ? p.Lastname.Contains(lastname) : false;
        }

        public bool IsPassengerByPassport(IPassenger p, string passport) {
            return p != null ? p.Passport.Contains(passport) : false;
        }

        public Queue<Flight> FindFlightsByPriceAndClass(FlightClass fc, decimal price) {
            Queue<Flight> founded = new Queue<Flight>();
            foreach (Flight f in _flights)
            {
                if (f.IsFlightEmpty())
                {
                    if (TicketManager.CalculateFullprice(fc, f.BasePriceOfTicket) < price)
                    {
                        founded.Enqueue(f);
                    }
                }
            }
            return founded;
        }

        public void PrintFlightsFromQueue(Queue<Flight> flights, FlightClass fc) {
            Console.WriteLine();
            Console.WriteLine(new string('-', 75));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  #  |    City   | Terminal | Gate |  Date and Time |   Class  | Price, $ | ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 75));
            while (flights.Count != 0)
            {
                Flight tempFlight = flights.Dequeue();
                Console.WriteLine(String.Format(" {0,-4}|  {1,-9}|     {2,-5}|  {3,-3} | {4:dd.MM.yy HH:mm} | {5, -8} |   {6, -6} |",
                           tempFlight.FlightNumber, tempFlight.City, tempFlight.Terminal,
                            tempFlight.Gate, tempFlight.DateAndTime,
                            fc, TicketManager.CalculateFullprice(fc, tempFlight.BasePriceOfTicket)));
                Console.WriteLine(new string('-', 75));
            }
        }

        public void PrintPassengersFromQueue(Queue<IPassenger> passengers) {
            Console.WriteLine();
            Console.WriteLine(new string('-', 102));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("#Seat| Firstname | Lastname   | Passport | Birthday | Sex    | Nationality |   Class  | Ticket Price |");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 102));
            int count = 0;
            if (passengers.Count == 0) Console.WriteLine("There are no results");
            while (passengers.Count != 0)
            {
                count++;
                if (passengers.Peek() != null)
                {
                    IPassenger tempPass = passengers.Dequeue();
                    Console.WriteLine(String.Format("  {0,-3}| {1,-10}| {2,-10} | {3,-8} | {4:dd.MM.yy} | {5,-6} | {6,-11} | {7,-8} |   {8, -8}$  |",
                                count, tempPass.Firstname, tempPass.Lastname, tempPass.Passport, tempPass.Birthday,
                                tempPass.Sex, tempPass.Nationality, tempPass.Ticket.FlightClass, tempPass.Ticket.FullPrice));
                    Console.WriteLine(new string('-', 102));
                }
            }
        }

        public void PrintAllFlights() {
            Console.WriteLine();
            Console.WriteLine(new string('-', 53));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  #  |    City   | Terminal | Gate |  Date and Time | ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 53));
            for (int i = 0; i < _flights.Length; i++)
            {
                if (_flights[i] != null)
                {
                    Console.WriteLine(String.Format(" {0,-4}|  {1,-9}|     {2,-5}|  {3,-3} | {4:dd.MM.yy HH:mm} | ",
                                        _flights[i].FlightNumber, _flights[i].City, _flights[i].Terminal,
                                        _flights[i].Gate, _flights[i].DateAndTime));
                    Console.WriteLine(new string('-', 53));
                }
            }
        }

        public void PrintEmptyFlights() {
            Console.WriteLine();
            Console.WriteLine(new string('-', 53));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  #  |    City   | Terminal | Gate |  Date and Time | ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 53));
            for (int i = 0; i < _flights.Length; i++)
            {
                if (_flights[i].IsFlightEmpty())
                {
                    Console.WriteLine(String.Format(" {0,-4}|  {1,-9}|     {2,-5}|  {3,-3} | {4:dd.MM.yy HH:mm} | ",
                                       _flights[i].FlightNumber, _flights[i].City, _flights[i].Terminal,
                                       _flights[i].Gate, _flights[i].DateAndTime));
                    Console.WriteLine(new string('-', 53));
                }
            }
        }

        public void PrintNotEmptyFlightsAverageAge() {
            Console.WriteLine();
            Console.WriteLine(new string('-', 62));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  #  |    City   | Terminal | Gate |  Date and Time |   Age  | ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 62));
            for (int i = 0; i < _flights.Length; i++)
            {
                if (_flights[i] != null)
                {
                    if (!_flights[i].IsFlightEmpty())
                    {

                        Console.WriteLine(" {0,-4}|  {1,-9}|     {2,-5}|  {3,-3} | {4:dd.MM.yy HH:mm} |  {5,-5} |",
                                           _flights[i].FlightNumber, _flights[i].City, _flights[i].Terminal,
                                           _flights[i].Gate, _flights[i].DateAndTime, _flights[i].AverageAgeOnFlight());
                        Console.WriteLine(new string('-', 62));
                    }
                }
            }
        }

        public void PrintNotEmptyFlightsTotalAmountMoney() {
            Console.WriteLine();
            Console.WriteLine(new string('-', 71));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  #  |    City   | Terminal | Gate |  Date and Time | Total amount, $ | ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 71));
            for (int i = 0; i < _flights.Length; i++)
            {
                if (_flights[i] != null)
                {
                    if (!_flights[i].IsFlightEmpty())
                    {
                        Console.WriteLine(" {0,-4}|  {1,-9}|     {2,-5}|  {3,-3} | {4:dd.MM.yy HH:mm} |     {5,-8}    |",
                                           _flights[i].FlightNumber, _flights[i].City, _flights[i].Terminal,
                                           _flights[i].Gate, _flights[i].DateAndTime, _flights[i].TotalAmountOfMoneyOnFlight());
                        Console.WriteLine(new string('-', 71));
                    }
                }
            }
        }

    }
}
