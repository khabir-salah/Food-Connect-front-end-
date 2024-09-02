using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Update
{
    public class ViewProfileViewModel
    {
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ProfileImage { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Name { get; set; }
        public string Nin { get; set; }
        public string? City { get; set; }
        public string? LOcalGovernment { get; set; }
        public string? PostalCode { get; set; }

        public string CacNumber { get; set; } = default!;
        public int? NumberOfPeopleInOrganization { get; set; }

        public int FamilySize { get; set; }
        public ICollection<FamilyHeadMemebers?> Families { get; set; }
    }

    public class FamilyHeadMemebers
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? Nin { get; set; }
    }

}
