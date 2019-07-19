using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TibcoCarRentals.Models
{
    public class MainViewModel
    {
        public List<Car> Cars;
        public List<Bus> Buses;
        public SelectList Models;
        public string CarModel { get; set; }
        public string SearchYear { get; set; }
        public string SearchPrice { get; set; }
        public string SearchSeats { get; set; }
    }
}
