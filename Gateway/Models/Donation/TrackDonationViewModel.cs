using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Donation
{
    public class TrackDonationViewModel
    {
        public DonationResponseCommandModel Donation {  get; set; }
        public MessageCommandModel Message { get; set; }
    }

    public class DonationResponseCommandModel
    {
        public string FoodDetails { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DonationStatus Status { get; set; }
        public DateTime PickUpTime { get; set; }
        public string PickUpLocation { get; set; } = default!;
        public ICollection<string> DonationImages { get; set; }
        public string PrimaryImageUrl { get; set; }
        public Guid DonationId { get; set; }
        public Guid RecipientId { get; set; }
    }

    public class MessageCommandModel
    {
        public Guid DonorId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid DonationId { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
