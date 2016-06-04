using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineLibrary.Passengers
{
    public class Passenger : IPassenger, IEquatable<Passenger>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Passport { get; set; }
        public DateTime Birthday { get; set; }
        public Sex Sex { get; set; }
        public Nationality Nationality { get; set; }
        public int Age { get { return age(); } }
        public ITicket Ticket { get; }

        private int age() {
            DateTime today = DateTime.Today;
            int age = today.Year - Birthday.Year;
            return Birthday > today.AddYears(-age) ? age-- : age;
        }

        public Passenger(string firstname, string lastname, string passport,
            DateTime birthday, Sex sex, Nationality nationality, ITicket ticket) {
            Firstname = firstname;
            Lastname = lastname;
            Passport = passport;
            Birthday = birthday;
            Nationality = nationality;
            Sex = sex;
            Ticket = ticket;
        }

        public override string ToString() {
            return String.Format($"Lastname: {Lastname} Name: {Firstname} Passport: {Passport}\n Birthday: {Birthday.ToShortDateString()}" +
                $" Sex: {Sex} Nationality: {Nationality}\n {Ticket.Info()}");
        }

        public bool Equals(Passenger other) {
            if (other == null) return false;
            bool b = this.Ticket.Id.Equals(other.Ticket.Id);
            return this.Ticket.Id.Equals(other.Ticket.Id);
        }

        public override bool Equals(object obj) {
            if (obj is Passenger)
            {
                Passenger p = (Passenger)obj;
                return Equals(p);
            }
            return false;
        }

        public override int GetHashCode() {
            //TODO ask about hashcoding
            return Firstname.GetHashCode() ^ Lastname.GetHashCode() ^ Passport.GetHashCode();
        }

        //TODO Ask about WTF?
        public static bool operator ==(Passenger p1, Passenger p2) {
            if (object.ReferenceEquals(p1, null))
            {
                return object.ReferenceEquals(p2, null);
            }
            return p1.Equals(p2);
        }

        public static bool operator !=(Passenger p1, Passenger p2) {
            return !(p1 == p2);
        }
    }
}
