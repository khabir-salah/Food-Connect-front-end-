using Gateway.Models.Donation;
using Gateway.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Services.Messaging
{
    public interface IMessaging_Service
    {
        Task<bool> SendMessage(SendDonationMessageModel request);
        Task<bool> MarkDonationAsReceived(MessageModel request, string token);
        Task<bool> MarkAvailable(MessageModelAvailable request, string token);
        Task<DonationWithMessagesView> GetDonationWithMessages(Guid donationId, string token);
        Task<ICollection<DonationReviewViewModel>> Review(string token);
        Task<ICollection<DonationReviewViewModel>> AllInappropriateReview(string token);
    }
}
        