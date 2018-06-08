using System;
using System.Collections.Generic;

namespace CarRental.Models
{
    public partial class Rent
    {
        public int IdRent { get; set; }
        public int? IdCar { get; set; }
        public string Login { get; set; }

        public Cars IdCarNavigation { get; set; }
        public Users LoginNavigation { get; set; }
    }
}
