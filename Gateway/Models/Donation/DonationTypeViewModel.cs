using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Donation
{
    public class DonationTypeViewModel
    {
        public int PendingCount { get; set; }
        public int ApproveCount { get; set; }
        public int DisapproveCount { get; set; }
        public int ReceivedCount { get; set; }
    }
}
