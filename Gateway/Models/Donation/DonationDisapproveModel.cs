using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Donation
{
    public class DonationDisapproveModel
    {
        public Guid Id { get; set; }
        public string ReasonForDisapproval { get; set; }
    }
}
