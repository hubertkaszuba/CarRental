using System;
using System.Collections.Generic;

namespace CarRental.Models
{
    public partial class Cars
    {
        public Cars()
        {
            Rent = new HashSet<Rent>();
        }

        public int IdCar { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public bool Available { get; set; }

        public ICollection<Rent> Rent { get; set; }
    }
}
