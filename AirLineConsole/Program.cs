using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineLibrary.Helpers;
using AirLineLibrary.Flights;
using AirLineLibrary.Passengers;
using System.Diagnostics;

namespace AirLineConsole
{
    class Program
    {
        static void Main(string[] args) {

            Trace.WriteLine(string.Format("Application started: {0}",DateTime.Now), "MyApp");

            #region ConsoleSettings
            Console.SetWindowSize(Math.Min(150, Console.LargestWindowWidth), Math.Min(50, Console.LargestWindowHeight));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = "Airline app petro";
            #endregion
            #region RandomGeneration

            Random rand = new Random(Seed: 10);

            FlightManager fm = new FlightManager();
            for (int i = 0; i < 5; i++)
            {
                Flight f2 = RandomGenerator.GenerateRandomFlightWithPassangers(rand, i, 10, 140);
                Flight f3 = RandomGenerator.GenerateRandomFlight(rand, 20 + i);
                fm.AddFlight(f2);
                fm.AddFlight(f3);
            }

            Console.Clear();
            #endregion

            UserInput.FlightManagerMenu(fm);

            Trace.WriteLine(string.Format("Application finished: {0}\n", DateTime.Now), "MyApp");
            Console.ReadLine();
        }
    }
}
