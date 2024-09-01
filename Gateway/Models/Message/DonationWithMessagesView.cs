using Gateway.Models.Donation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Message
{
    public class DonationWithMessagesView
    {
        public DonationResponseCommandModel Donation { get; set; }
        public ICollection<MessageCommandModel> Messages { get; set; }
        public bool IsDonor { get; set; }
    }
}
