using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Donation
{
    public class SendDonationMessageModel
    {
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid DonationId { get; set; }
        public string Content { get; set; }
    }

    
}
