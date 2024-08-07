using Gateway.Models.Donation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Services.FoodDonation
{
    public interface IFoodDonationService
    {
        Task<bool> CreateDonation(CreateDonationModels request, string token);

        Task<DonationTypeViewModel> ViewDonationType(string token);

        Task<UserDonationViewModel> UserPendingDonations( string token);

        Task<UserDonationViewModel> UserApprovedDonations();


        Task<UserDonationViewModel> UserReceivedDonations();

        Task<UserDonationViewModel> UserDisapprovedDonations();

        Task<TrackDonationViewModel> TrackDonation(Guid donationId);

        Task<UserDonationViewModel> GetClaimedDonations();

        Task<bool> SendMessage(SendDonationMessageModel model);

        Task<UserDonationViewModel> ReceiveFoodDonation();

        Task<DonationResponseCommandModel> SearchDonations(string location, int? minQuantity, int? maxQuantity);



    }
}
