﻿using System;
using System.Collections.Generic;
using AirportBookingSystem.Models;
namespace AirportBookingSystem
{
class Program
    {
    static void Main()
        {
            Console.WriteLine("Welcome to the Airport Ticket Booking System");

            Console.WriteLine("Are you a:");
            Console.WriteLine("1. Passenger");
            Console.WriteLine("2. Manager");



            switch ( Console.ReadLine())
            {
                case "1":
                    PassengerMenu();
                    break;
                case "2":
                    ManagerMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }

        static void PassengerMenu()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your Passport Number: ");
            string passport = Console.ReadLine();

            Passenger passenger = new Passenger
            {
                Name = name,
                PassportNumber = passport
            };

            Manager manager = new Manager();
            List<Flight> flights = manager.LoadFlights();

            if (flights.Count == 0)
            {
                Console.WriteLine("No flights available.");
                return;
            }

            foreach (var f in flights)
            {
                Console.WriteLine($"ID: {f.FlightId}");
                Console.WriteLine($"From: {f.DepartureCountry} ");
                Console.WriteLine($"Airport: ({f.DepartureAirport})");

                Console.WriteLine($"To: {f.DestinationCountry} ");
                Console.WriteLine($"Airport: ({f.ArrivalAirport})");

                Console.WriteLine($"Departure Date: {f.DepartureDate.ToShortDateString()}");
                Console.WriteLine($"Economy: {f.PriceEconomy}, Business: {f.PriceBusiness}, First: {f.PriceFirstClass}");
            }

            Console.Write("\nEnter Flight ID to book: ");
            string flightId = Console.ReadLine();
            var selectedFlight = flights.Find(f => f.FlightId == flightId);

            if (selectedFlight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            Console.Write("Choose class (Economy, Business, FirstClass): ");
            string classInput = Console.ReadLine();

            if (!Enum.TryParse(classInput, true, out BookingClass bookingClass))
            {
                Console.WriteLine("Invalid class.");
                return;
            }
            
            Booking booking = new Booking
            {
                BookingId = Guid.NewGuid().ToString(),
                Passenger = passenger,
                Flight = selectedFlight,
                BookingClass = bookingClass,
                BookingDate = DateTime.Now
            };

            
            Console.WriteLine("\nBooking Successful!");
            Console.WriteLine($"Booking ID: {booking.BookingId}");
            Console.WriteLine($"Passenger: {passenger.Name}");
            Console.WriteLine($"Flight: {selectedFlight.FlightId} | Class: {bookingClass}");
        }
        static void ManagerMenu()
        {
                    Manager manager = new Manager();


            while (true)
            {
                Console.WriteLine("\nManager Menu:");
                Console.WriteLine("1. Import Flights from CSV");
                Console.WriteLine("2. View All Flights");
                Console.WriteLine("3. Add Flight Manually");
                Console.WriteLine("4. Return to Main Menu");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                 switch (choice)
                {
                    case "1":
                        Console.Write("Enter CSV file path: ");
                        string path = Console.ReadLine();
                        manager.ImportFlightsFromCSV(path);
                        break;
                    case "2":
                        var flights = manager.LoadFlights();
                        if (flights.Count == 0)
                        {
                            Console.WriteLine("No flights available.");
                        }
                        else
                        {
                            foreach (var f in flights)
                            {
                                Console.WriteLine($"ID: {f.FlightId}");
                                Console.WriteLine($"From: {f.DepartureCountry} ({f.DepartureAirport})");
                                Console.WriteLine($"To: {f.DestinationCountry} ({f.ArrivalAirport})");
                                Console.WriteLine($"Date: {f.DepartureDate.ToShortDateString()}");
                                Console.WriteLine($"Economy: {f.PriceEconomy}, Business: {f.PriceBusiness}, First: {f.PriceFirstClass}");
                            }
                        }
                        break;

                    case "3":
                        Flight newFlight = new Flight();
                        Console.Write("Departure Country: ");
                        newFlight.DepartureCountry = Console.ReadLine();

                        Console.Write("Destination Country: ");
                        newFlight.DestinationCountry = Console.ReadLine();

                        Console.Write("Departure Airport: ");
                        newFlight.DepartureAirport = Console.ReadLine();
                        Console.Write("Arrival Airport: ");
                        newFlight.ArrivalAirport = Console.ReadLine();

                        Console.Write("Departure Date (yyyy-MM-dd): ");
                        newFlight.DepartureDate = DateTime.Parse(Console.ReadLine());

                        Console.Write("Economy Price: ");
                        newFlight.PriceEconomy = decimal.Parse(Console.ReadLine());

                        Console.Write("Business Price: ");
                        newFlight.PriceBusiness = decimal.Parse(Console.ReadLine());
                         Console.Write("First Class Price: ");
                         newFlight.PriceFirstClass = decimal.Parse(Console.ReadLine());

                         newFlight.FlightId = Guid.NewGuid().ToString();

                          manager.AddFlight(newFlight);
                          Console.WriteLine("Flight added successfully!");
                        break;
        }       
}       
        }}}

        