using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Registration
{
    public class AllUsersViewModel
    {
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string? Name { get; set; }
        public bool IsActivated { get; set; }
        public string Role { get; set; }
        public Guid UserId { get; set; }

    }
}
