using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Update
{
    public record UserDeatilsModel
    {
        public string Email { get; set; } 
        public string Password { get; set; } 
        public bool IsEmailConfirmed { get; set; } = false;
        public string PhoneNumber { get; set; } 
        public string? Address { get; set; }
        public string? Name { get; set; }
        public bool IsActivated { get; set; }
        public string OganisationName { get; set; } 
        public Guid UserId { get; set; }
        public string CacNumber { get; set; } 
        public string? City { get; set; }
        public string? LOcalGovernment { get; set; }
        public string? PostalCode { get; set; }
        public int? NumberOfPeopleInOrganization { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string? Nin { get; set; }
        public int NumberOfPersons { get; set; } = 1;
        public int FamilySize { get; set; }
        public int DonationsReceived { get; set; }
        public int DonationsDonated { get; set; }
        public string Role { get; set; }
    }
}
