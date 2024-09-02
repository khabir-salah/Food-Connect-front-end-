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
        Task<PaginatedResponse<UserDonationViewModel>> ReceivableFoodDonation(string token, int PageNumber = 1, int PageSize = 30);
        Task<DonationTypeViewModel> DonationCount(string token);
        Task<bool> DispproveByManager(DonationDisapproveModel request, string token);
        Task<ICollection<UserDonationViewModel>> UserExpiredDonations(string token);
        Task<ICollection<UserDonationViewModel>> UserPendingDonations( string token);
        Task<bool> ApproveByManager(Guid donationId, string token);
        Task<PaginatedResponse<UserDonationViewModel>> SearchDonations(string token, string location, int? minQuantity, int? maxQuantity);
        Task<PaginatedResponse<AllDonationViewModel>> AllDonations(DonationsModel choice,  string token, int PageNumber , int PageSize);

        Task<ICollection<UserDonationViewModel>> UserApprovedDonations(string token);


        Task<ICollection<UserDonationViewModel>> UserReceivedDonations(string token);

        Task<ICollection<UserDonationViewModel>> UserDisapprovedDonations(string token);

        Task<TrackDonationViewModel> TrackDonation(Guid donationId);

        Task<ICollection<UserDonationViewModel>> GetClaimedDonations(string token);
        Task<ICollection<UserDonationViewModel>> GetClaimedDonationsByOther(string token);
        Task<bool> ClaimDonation(Guid donationId, string token);

        Task<ICollection<UserDonationViewModel>> SearchAllDonations(string token, string location, int? minQuantity, int? maxQuantity);
        Task<PaginatedResponse<UserDonationViewModel>> ReceiveFoodDonation(string token, int PageNumber = 1, int PageSize = 30);




    }
}
