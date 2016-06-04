using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineLibrary.Passengers;
using AirLineLibrary.Helpers;

namespace AirLineLibrary.Flights
{
    public class Flight
    {
        #region properties
        public int FlightNumber { get; set; }
        public ushort SeatsOnBoard { get; set; }
        public FlightStatus Status { get; set; }
        public FlightType FlightType { get; set; }
        public DateTime DateAndTime { get; set; }
        public string City { get; set; }
        public int Terminal { get; set; }
        public int Gate { get; set; }

        // Base price of the ticket. Wicht is set by airline company
        private decimal basePriceOfTicket;

        public decimal BasePriceOfTicket
        {
            get { return basePriceOfTicket; }
            //
            set { basePriceOfTicket = (value > 0) ? value : 50; }
        }

        public IPassenger[] GetPassengers { get { return _passengers; } }

        #endregion

        // TODO private
        private IPassenger[] _passengers;

        // There are no plane with more than 800 seats, i guess
        const ushort maxPlacesOnPlane = 800;

        #region constructors
        //TODO comment this
        public Flight(int flightNumber, ushort placesOnBoard, FlightStatus status,
                      FlightType flightType, DateTime dateAndTime,
                      string city, int terminal, int gate, decimal basePriceOfTicket) {
            SeatsOnBoard = (placesOnBoard > maxPlacesOnPlane || placesOnBoard < 10) ? maxPlacesOnPlane : placesOnBoard;

            FlightNumber = flightNumber;
            Status = status;
            FlightType = flightType;
            City = city;
            Terminal = terminal;
            Gate = gate;
            DateAndTime = dateAndTime;
            BasePriceOfTicket = basePriceOfTicket;
            _passengers = new Passenger[SeatsOnBoard];
        }

        public Flight(int flightNumber, ushort seatsOnBoard, FlightStatus status, FlightType flightType,
                        DateTime dateAndTime, string city,
                        int terminal, int gate, decimal basePriceOfTicket, Passenger[] passangers)
            : this(flightNumber, seatsOnBoard, status, flightType,
                  dateAndTime, city, terminal, gate, basePriceOfTicket) {
            if (passangers.Length > SeatsOnBoard)
            {
                _passengers = new Passenger[maxPlacesOnPlane];
                Console.WriteLine("Passengers were not added. Max size of passengers on board is {0}", SeatsOnBoard);
            }
            _passengers = passangers;
        }
        #endregion        

        public void AddPassenger(IPassenger p) {
            p.Ticket.BasePrice = BasePriceOfTicket;
            int index;
            // If there are passenger with the same ticket id
            bool isAlreadyOnboard = isPassengerWithTheSameTicketId(p.Ticket.Id);
            bool isFree = isFreeSeatAvaliable(out index);

            if (isAlreadyOnboard)
            {
                Console.WriteLine("Sorry, This passenger is already onboard");
            }
            if (isFree)
            {
                _passengers[index] = p;
                Console.WriteLine("Passenger was added!");
            } else
            {
                Console.WriteLine("Passenger was not added! There are no free seats. Sorry.");
            }
        }

        public void AddUserInputPassenger() {
            Passenger p = createPassenger();
            AddPassenger(p);
        }

        public void EditUserInputPassenger() {
            IPassenger editedPassenger = null;
            Console.WriteLine("Enter seat of the passenger: ");
            int seat = UserInput.ValidateNum<int>(int.TryParse);

            // bool isSelectedPassenger = seat < 0 || seat > _passengers.Length - 1 || _passengers[seat] == null;
            bool isSelectedPassenger = IsPassengerBySeatNum(seat, out editedPassenger);

            while (!isSelectedPassenger)
            {
                Console.WriteLine("Sorry, wrong seat number or empty seat! Try again: ");
                Console.WriteLine("Enter seat of the passenger: ");
                seat = UserInput.ValidateNum<int>(int.TryParse);
                // isSelectedPassenger = seat < 0 || seat > _passengers.Length - 1 || _passengers[seat] == null;
                isSelectedPassenger = IsPassengerBySeatNum(seat, out editedPassenger);
            }
            editedPassenger = _passengers[seat];
            Console.Write("Change firstname of the passenger: ");
            editedPassenger.Firstname = Console.ReadLine();

            Console.Write("Change lastname of the passenger: ");
            editedPassenger.Lastname = Console.ReadLine();

            Console.Write("Change firstname of the passenger (Example: PQ1234): ");
            editedPassenger.Passport = Console.ReadLine();

            Console.Write("Change birthday date of the passenger: ");
            editedPassenger.Birthday = UserInput.ValidateDate();

            Console.WriteLine("Change sex of the passenger: ");
            editedPassenger.Sex = UserInput.ValidateEnum<Sex>();

            Console.WriteLine("Change nationality of the passenger: ");
            editedPassenger.Nationality = UserInput.ValidateEnum<Nationality>();

            Console.WriteLine("Change flight class of the passenger: ");
            editedPassenger.Ticket.FlightClass = UserInput.ValidateEnum<FlightClass>();

            Console.WriteLine("Change base price of ticket, $: ");
            decimal basePriceOfTicket = UserInput.ValidateNum<decimal>(decimal.TryParse);
            while (basePriceOfTicket < 0)
            {
                Console.WriteLine("Base price of the flight ticket should be greater than 0! Try again!");
                Console.WriteLine("Change base price of the flight ticket, $: ");
                basePriceOfTicket = UserInput.ValidateNum<decimal>(decimal.TryParse);
            }
            editedPassenger.Ticket.BasePrice = basePriceOfTicket;
        }

        public void DeleteUserInputPassenger() {

            IPassenger deletePassenger = null;
            Console.WriteLine("Enter seat of the passenger: ");
            int seat = UserInput.ValidateNum<int>(int.TryParse);

            bool isSelectedPassenger = IsPassengerBySeatNum(seat, out deletePassenger);
            while (!isSelectedPassenger)
            {
                Console.WriteLine("Sorry, wrong seat number or empty seat! Try again: ");
                Console.WriteLine("Enter seat of the passenger: ");
                seat = UserInput.ValidateNum<int>(int.TryParse);
                isSelectedPassenger = IsPassengerBySeatNum(seat, out deletePassenger);
            }

            _passengers[seat] = null;

        }

        private Passenger createPassenger() {

            Console.Write("Enter firstname of the passenger: ");
            string firstname = Console.ReadLine();

            Console.Write("Enter lastname of the passenger: ");
            string lastname = Console.ReadLine();

            Console.Write("Enter firstname of the passenger (Example: PQ1234): ");
            string passport = Console.ReadLine();

            Console.Write("Enter birthday date of the passenger: ");
            DateTime birthday = UserInput.ValidateDate();

            Console.WriteLine("Enter sex of the passenger: ");
            Sex sex = UserInput.ValidateEnum<Sex>();

            Console.WriteLine("Enter nationality of the passenger: ");
            Nationality nationality = UserInput.ValidateEnum<Nationality>();

            Console.WriteLine("Enter flight class of the passenger: ");
            FlightClass flightClass = UserInput.ValidateEnum<FlightClass>();

            Console.WriteLine("Enter base price of ticket, $: ");
            decimal basePriceOfTicket = UserInput.ValidateNum<decimal>(decimal.TryParse);
            while (basePriceOfTicket < 0)
            {
                Console.WriteLine("Base price of the flight ticket should be greater than 0! Try again!");
                Console.WriteLine("Enter base price of the flight ticket, $: ");
                basePriceOfTicket = UserInput.ValidateNum<decimal>(decimal.TryParse);
            }

            return new Passenger(firstname, lastname, passport, birthday, sex, nationality,
                new Ticket(flightClass, basePriceOfTicket));
        }

        public void EditPassenger(int seatNum, IPassenger otherInst) {
            // If edited passenger is already somewhere on the flight you cannot edit
            bool isNoCollision = _passengers.Where(x => x == otherInst).Count() > 1;
            if (!(isNoCollision))
            {
                _passengers[seatNum] = otherInst;
            } else
            {
                Console.WriteLine("Sorry, edited passenger is already somewhere on the flight you cannot edit!");
            }
            IPassenger EditPassenger = _passengers[seatNum];

        }

        public void DeletePassenger(IPassenger p) {
            for (int i = 0; i < _passengers.Length; i++)
            {
                if (_passengers[i].Equals(p))
                {
                    Console.WriteLine("Passenger deleted!\n{0}", p.ToString());
                    _passengers[i] = null;
                    return;
                }
            }
            Console.WriteLine("Passenger was NOT deleted!");
        }

        public bool IsPassengerBySeatNum(int index, out IPassenger p) {
            p = null;

            if ((index < 0 || index > _passengers.Length - 1 || _passengers[index] == null))
            {
                Console.WriteLine("Wrong input or there are no passengers on seat #{0}\nTry again!", index);
                return false;
            } else
            {
                p = _passengers[index];
                return true;
            }
        }

        public void SearchByPassenger(IPassenger p, out IPassenger result, out int seat) {
            seat = -1;
            result = null;
            for (int i = 0; i < _passengers.Length; i++)
            {
                if (_passengers[i] == p)
                {
                    result = _passengers[i];
                    seat = i;
                    return;
                }
            }
            Console.WriteLine("No passengers found. Try again!");
        }

        public void SearchByTicket(ITicket ticket, out IPassenger result) {
            result = null;
            if (ticket == null)
            {
                foreach (Passenger p in _passengers)
                {
                    if (p.Ticket.Id.Equals(ticket.Id))
                    {
                        result = p;
                        return;
                    }
                }
            }
            Console.WriteLine("No passengers found. Try again!");
        }

        public override string ToString() {
            return string.Format($"Flight #{FlightNumber} Status: {Status} Time of {FlightType}: {DateAndTime.ToShortDateString()}" +
                $"{DateAndTime.ToShortTimeString()}\nCity: {City} Terminal: {Terminal} Gate: {Gate}");
        }

        public void PrintPassengers() {
            Console.WriteLine();
            Console.WriteLine(new string('-', 87));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("#Seat| Firstname | Lastname   | Passport | Birthday | Sex    | Nationality |   Class  |");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 87));
            for (int i = 0; i < _passengers.Length; i++)
            {
                if (_passengers[i] != null)
                {
                    Console.WriteLine(String.Format("  {0,-3}| {1,-10}| {2,-10} | {3,-8} | {4:dd.MM.yy} | {5,-6} | {6,-11} | {7,-8} |",
                                i, _passengers[i].Firstname, _passengers[i].Lastname, _passengers[i].Passport, _passengers[i].Birthday,
                                _passengers[i].Sex, _passengers[i].Nationality, _passengers[i].Ticket.FlightClass));
                    Console.WriteLine(new string('-', 87));
                }
            }
        }

        public void PrintPassengersTicketInfo() {
            Console.WriteLine();
            Console.WriteLine(new string('-', 100));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("#Seat| Firstname | Lastname   |             Ticket Id            |   Class   | Price, $ | Discount |   ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 100));
            for (int i = 0; i < _passengers.Length; i++)
            {
                if (_passengers[i] != null)
                {
                    Console.WriteLine(String.Format("  {0,-3}| {1,-10}| {2,-10} | {3,-32} | {4,-9} | {5,-8} |   {6,-6} | ",
                                i, _passengers[i].Firstname, _passengers[i].Lastname, _passengers[i].Ticket.Id,
                                _passengers[i].Ticket.FlightClass, _passengers[i].Ticket.FullPrice, _passengers[i].Ticket.Discount));
                    Console.WriteLine(new string('-', 100));
                }
            }
        }

        private bool isPassengerWithTheSameTicketId(string ticketID) {
            for (int i = 0; i < _passengers.Length; i++)
            {
                if (_passengers[i] != null)
                {
                    if (_passengers[i].Ticket.Id.Equals(ticketID))
                        return true;
                }
            }
            return false;
        }

        // out of task, just bonus
        public decimal TotalAmountOfMoneyOnFlight() {
            return _passengers.Where(x => x != null).Sum(x => x.Ticket.FullPrice);
        }

        // out of task, just bonus
        public double AverageAgeOnFlight() {
            return Math.Round(_passengers.Where(x => x != null).Average(x => x.Age), 2);
        }

        private bool isFreeSeatAvaliable(out int index) {
            index = -1;
            for (int i = 0; i < _passengers.Length; i++)
            {
                if (_passengers[i] == null)
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }

        // gets the full price of the ticket derived from ITicket depeding from FlightClass
        //private decimal getTicketPrice<T>(FlightClass fc) where T : ITicket, new()
        //{
        //    T ticket = new T();
        //    ticket.FlightClass = fc;
        //    ticket.BasePrice = BasePriceOfTicket;
        //    return ticket.FullPrice;
        //}

        //public decimal GetFullTicketPrice(FlightClass fc)
        //{
        //    _sampleTicket.FlightClass = fc;
        //    _sampleTicket.BasePrice = BasePriceOfTicket;
        //    return _sampleTicket.FullPrice;
        //}

        public bool IsFlightEmpty() {
            foreach (IPassenger p in _passengers)
            {
                if (p != null)
                    return false;
            }
            return true;
        }

    }
}
