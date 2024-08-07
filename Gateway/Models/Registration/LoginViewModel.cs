using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Organisation
{
        public record LoginViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginResponse
        {
            public string Token { get; set; }
            public string Message { get; set; }
        }
       

}
