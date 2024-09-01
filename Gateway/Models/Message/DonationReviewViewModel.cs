using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Message
{
    public class DonationReviewViewModel
    {
        public string? Review { get; set; }
        public string FoodDetails { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string PickUpLocation { get; set; } = default!;
        public string PrimaryImageUrl { get; set; } = default!;
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
    }
}
