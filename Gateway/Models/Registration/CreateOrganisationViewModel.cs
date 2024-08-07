using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Organisation
{
    public class CreateOrganisationViewModel
    {
        public string OganisationName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string CacNumber { get; set; } = default!;
        public int? Capacity { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
