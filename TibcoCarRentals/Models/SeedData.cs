using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TibcoCarRentals.Data;

namespace TibcoCarRentals.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any cars / buses.
                if (context.Car.Any() && context.Bus.Any())
                {
                    return;   // DB has been seeded
                }

                context.Car.AddRange(
                    new Car
                    {
                        Model = "Honda",
                        Year = 2014,
                        DailyPrice = 100,
                        NumSeats = 5,
                        Warrent = DateTime.Parse("11-12-2014"),
                        Registration = DateTime.Parse("11-12-2014"),
                        ImageLocation = "/images/car/car2.jpg",
                        Quantity = 2
                    },

                    new Car
                    {

                        Model = "Madza",
                        Year = 2019,
                        DailyPrice = 105,
                        NumSeats = 5,
                        Warrent = DateTime.Parse("11-12-2014"),
                        Registration = DateTime.Parse("11-12-2014"),
                        ImageLocation = "/images/car/redcar.jpg",
                        Quantity = 1
                    },

                    new Car
                    {
                        Model = "BMW",
                        Year = 2018,
                        DailyPrice = 150,
                        NumSeats = 5,
                        Warrent = DateTime.Parse("11-12-2014"),
                        Registration = DateTime.Parse("11-12-2014"),
                        ImageLocation = "/images/car/car6.jpg",
                        Quantity = 7
                    },

                    new Car
                    {
                        Model = "Nissan",
                        Year = 2010,
                        DailyPrice = 80,
                        NumSeats = 5,
                        Warrent = DateTime.Parse("11-12-2014"),
                        Registration = DateTime.Parse("11-12-2014"),
                        ImageLocation = "/images/car/car7.jpg",
                        Quantity = 6
                    },
                     new Car
                     {
                         Model = "Suzuki",
                         Year = 2014,
                         DailyPrice = 100,
                         NumSeats = 7,
                         Warrent = DateTime.Parse("11-12-2014"),
                         Registration = DateTime.Parse("11-12-2014"),
                         ImageLocation = "/images/car/car11.jpg",
                         Quantity = 4

                     },
                      new Car
                      {
                          Model = "Toyota",
                          Year = 2019,
                          DailyPrice = 170,
                          NumSeats = 5,
                          Warrent = DateTime.Parse("11-12-2014"),
                          Registration = DateTime.Parse("11-12-2014"),
                          ImageLocation = "/images/car/unavailable.jpg",
                          Quantity = 8

                      }
                );

                context.Bus.AddRange(
                    new Bus
                    {
                        Model = "Bussiness Bus",
                        Year = 2014,
                        DailyPrice = 100,
                        NumSeats = 5,
                        Warrent = DateTime.Parse("11-12-2014"),
                        Registration = DateTime.Parse("11-12-2014"),
                        ImageLocation = "/images/bus/blue-bus.jpg",
                        Quantity = 4
                    },

                    new Bus
                    {

                        Model = "Bulli",
                        Year = 2019,
                        DailyPrice = 155,
                        NumSeats = 5,
                        Warrent = DateTime.Parse("11-12-2014"),
                        Registration = DateTime.Parse("11-12-2014"),
                        ImageLocation = "/images/bus/bulli.png",
                        Quantity = 6
                    },

                    new Bus
                    {
                        Model = "Coach",
                        Year = 2018,
                        DailyPrice = 450,
                        NumSeats = 5,
                        Warrent = DateTime.Parse("11-12-2014"),
                        Registration = DateTime.Parse("11-12-2014"),
                        ImageLocation = "/images/bus/coach.png",
                        Quantity = 2
                    },

                    new Bus
                    {
                        Model = "Volkswagen",
                        Year = 2010,
                        DailyPrice = 800,
                        NumSeats = 5,
                        Warrent = DateTime.Parse("11-12-2014"),
                        Registration = DateTime.Parse("11-12-2014"),
                        ImageLocation = "/images/bus/volkswagen-1.png",
                        Quantity = 2
                    },
                     new Bus
                     {
                         Model = "Volkswagen",
                         Year = 2014,
                         DailyPrice = 200,
                         NumSeats = 30,
                         Warrent = DateTime.Parse("11-12-2014"),
                         Registration = DateTime.Parse("11-12-2014"),
                         ImageLocation = "/images/bus/volkswagen-2.jpg",
                         Quantity = 2
                     },
                      new Bus
                      {
                          Model = "Party Bus",
                          Year = 2019,
                          DailyPrice = 170,
                          NumSeats = 10,
                          Warrent = DateTime.Parse("11-12-2014"),
                          Registration = DateTime.Parse("11-12-2014"),
                          ImageLocation = "/images/car/unavailable.jpg",
                          Quantity = 2
                      }
                );
                context.SaveChanges();
            }
        }
    }
}