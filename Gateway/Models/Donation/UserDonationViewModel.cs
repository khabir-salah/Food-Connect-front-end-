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
        public string? DonationImages { get; set; } 
        public string PrimaryImageUrl { get; set; } = null!;
        public string ClaimRestrictionReason { get; set; }
        public bool CanClaim { get; set; }
        public string  UserEmail { get; set; }
        public string  UserRole { get; set; }
        public string  Name { get; set; }
        public string  DonationMadeBy { get; set; }
        public Guid  DonationId { get; set; }
        public string? RecipientName { get; set; }
        public string? RecipientEmail { get; set; }
        public string? RecipientRole { get; set; }
        public string? Address { get; set; }
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
