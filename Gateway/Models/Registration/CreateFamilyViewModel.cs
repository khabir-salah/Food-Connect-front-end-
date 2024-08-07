using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Registration
{
    public class CreateFamilyViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FamilyCount { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NIN { get; set; }
        public string Password { get; set; }
    }
}
