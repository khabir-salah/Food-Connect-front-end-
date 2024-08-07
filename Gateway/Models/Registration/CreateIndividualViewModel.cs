using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Registration
{
    public class CreateIndividualViewModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Nin { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
