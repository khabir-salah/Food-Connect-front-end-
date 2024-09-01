using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Message
{
    public class MessageModel
    {
        public Guid DonationId { get; set; }
        public string Review { get; set; }
    }

    public class MessageModelAvailable
    {
        public Guid DonationId { get; set; }
        public string reason { get; set; }
    }
}
