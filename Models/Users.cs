using System;
using System.Collections.Generic;


namespace CarRental.Models
{
    public partial class Users 
    {
        public Users()
        {
            Rent = new HashSet<Rent>();
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public int? PhoneNumber { get; set; }

        public ICollection<Rent> Rent { get; set; }
    }
}
