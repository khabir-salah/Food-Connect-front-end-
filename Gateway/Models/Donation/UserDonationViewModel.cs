using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Donation
{
    public class UserDonationViewModel
    {
        public string FoodDetails { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DonationStatus Status { get; set; }
        public DateTime PickUpTime { get; set; }
        public string PickUpLocation { get; set; } = default!;
        public ICollection<string> DonationImages { get; set; } = null!;
        public string PrimaryImageUrl { get; set; } = null!;
        public string ClaimRestrictionReason { get; set; }
        public bool CanClaim { get; set; }
    }

    public enum DonationStatus
    {
        pending,
        Approve,
        Disapprove,
        Available,
        Claim,
        Received
    }
}
