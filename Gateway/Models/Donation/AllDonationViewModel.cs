using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Donation
{
    public class AllDonationViewModel
    {
        public string FoodDetails { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DonationStatus Status { get; set; }
        public DateTime PickUpTime { get; set; }
        public string PickUpLocation { get; set; } = default!;
        public string DonationImages { get; set; } 
        public string PrimaryImageUrl { get; set; } = null!;
        public string? DonationMadeBy { get; set; }
        public string? UserEmail { get; set; }
        public string? UserRole { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? profileImage { get; set; }
        public string? Address { get; set; }
        public string? ReasonForDisapproval { get; set; }
        public string? ManagerName { get; set; }
        public string? ManagerEmail { get; set; }
        public string? RecipientName { get; set; }
        public string? RecipientEmail { get; set; }
        public bool CanClaim { get; set; }
        public string ClaimRestrictionReason { get; set; }
        public Guid DonationId { get; set; }
    }

    public class PaginatedResponse<T>
    {
        public int PageNumber { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public ICollection<T> Data { get; set; } 
    }
}
