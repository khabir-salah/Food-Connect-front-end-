using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Message
{
    public class MessageViewModel
    {
        public Guid DonorId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid DonationId { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
