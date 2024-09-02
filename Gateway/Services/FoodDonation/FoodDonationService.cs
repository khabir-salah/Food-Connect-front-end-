using Gateway.Extension;
using Gateway.Models.Donation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gateway.Services.FoodDonation
{
    public class FoodDonationService : IFoodDonationService
    {

        private readonly HttpClient _httpClient;
        public FoodDonationService()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromDays(7)
            };
            
        }

        public async Task<PaginatedResponse<AllDonationViewModel>> AllDonations(DonationsModel choice, string token, int PageNumber = 1, int PageSize = 30)
        {
            var apiUrl = $"https://localhost:7005/api/Donation/AllDonations?Status={choice.status}&PageNumber={PageNumber}&PageSize={PageSize}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<PaginatedResponse<AllDonationViewModel>>();
            return result;
        }

        public async Task<PaginatedResponse<UserDonationViewModel>> ReceiveFoodDonation(string token, int PageNumber = 1, int PageSize = 30)
        {
            var apiUrl = $"https://localhost:7005/api/Donation/AllUserClaimableDonations?PageNumber={PageNumber}&PageSize={PageSize}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<PaginatedResponse<UserDonationViewModel>>();
            return result;
        }

        public async Task<PaginatedResponse<UserDonationViewModel>> ReceivableFoodDonation(string token, int PageNumber = 1, int PageSize = 30)
        {
            var apiUrl = $"https://localhost:7005/api/Donation/AllUserDonations?PageNumber={PageNumber}&PageSize={PageSize}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<PaginatedResponse<UserDonationViewModel>>();
            return result;
        }

        public async Task<bool> CreateDonation(CreateDonationModels request, string token)
        {
            //string json = JsonSerializer.Serialize(request);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://localhost:7005/api/Donation/Create";

            using (var content = new MultipartFormDataContent())
            {
                // Add simple fields
                content.Add(new StringContent(request.FoodDetails), nameof(request.FoodDetails));
                content.Add(new StringContent(request.Quantity.ToString()), nameof(request.Quantity));
                content.Add(new StringContent(request.ExpirationDate.ToString("o")), nameof(request.ExpirationDate)); 
                content.Add(new StringContent(request.PickUpTime.ToString("o")), nameof(request.PickUpTime));
                content.Add(new StringContent(request.PickUpLocation), nameof(request.PickUpLocation));
                content.Add(new StringContent(request.UserId.ToString()), nameof(request.UserId));

                // Add primary image
                if (request.PrimaryImageUrl != null)
                {
                    var primaryImageContent = new StreamContent(request.PrimaryImageUrl.OpenReadStream());
                    var ImageContent = new StreamContent(request.DonationImages.OpenReadStream());
                    primaryImageContent.Headers.ContentType = new MediaTypeHeaderValue(request.PrimaryImageUrl.ContentType);
                    ImageContent.Headers.ContentType = new MediaTypeHeaderValue(request.DonationImages.ContentType);
                    content.Add(primaryImageContent, nameof(request.PrimaryImageUrl), request.PrimaryImageUrl.FileName);
                    content.Add(ImageContent, nameof(request.DonationImages), request.DonationImages.FileName);
                }

                // Add additional images
                //foreach (var file in request.DonationImages)
                //{
                //    var fileContent = new StreamContent(file.OpenReadStream());
                //    fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                //    content.Add(fileContent, nameof(request.DonationImages), file.FileName);
                //}
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsync(url, content);
                return response.IsSuccessStatusCode ? true : false;
            }
            

        }

        public async Task<ICollection<UserDonationViewModel>> GetClaimedDonations(string token)
        {
            var apiUrl = "https://localhost:7005/api/Donation/claimedbyUser";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new List<UserDonationViewModel>();
            }
            var result = await response.ReadContentAs<ICollection<UserDonationViewModel>>();
            return result ?? new List<UserDonationViewModel>();
        }

        public async Task<ICollection<UserDonationViewModel>> GetClaimedDonationsByOther(string token)
        {
            var apiUrl = "https://localhost:7005/api/Donation/claimedbyOtherUser";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new List<UserDonationViewModel>();
            }
            var result = await response.ReadContentAs<ICollection<UserDonationViewModel>>();
            return result ?? new List<UserDonationViewModel>();
        }



        public async Task<PaginatedResponse<UserDonationViewModel>> SearchDonations(string token, string location, int? minQuantity, int? maxQuantity)
        {
            var queryString = $"?location={location}&minQuantity={minQuantity}&maxQuantity={maxQuantity}";
            var apiUrl = $"https://localhost:7005/api/Donation/Search{queryString}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<PaginatedResponse<UserDonationViewModel>>();
            return result;
        }

        public async Task<ICollection<UserDonationViewModel>> SearchAllDonations(string token, string location, int? minQuantity, int? maxQuantity)
        {
            var queryString = $"?location={location}&minQuantity={minQuantity}&maxQuantity={maxQuantity}";
            var apiUrl = $"https://localhost:7005/api/Donation/AllSearch{queryString}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<ICollection<UserDonationViewModel>>();
            return result;
        }




        public async Task<TrackDonationViewModel> TrackDonation(Guid donationId)
        {
            var response = await _httpClient.GetAsync($"api/Donation/{donationId}");
            var result = await response.ReadContentAs<TrackDonationViewModel>();
            return result;
        }

        public async Task<bool> ApproveByManager(Guid donationId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsync($"https://localHost:7005/api/Donation/approve/{donationId}", null);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> ClaimDonation(Guid donationId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsync($"https://localHost:7005/api/Donation/Claim/{donationId}", null);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> DispproveByManager(DonationDisapproveModel request, string token)
        {
            string json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://localhost:7005/api/Donation/Disapprove";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode ? true : false;
        }
        

         public async Task<ICollection<UserDonationViewModel>> UserExpiredDonations(string token)
        {
            var apiUrl = "https://localhost:7005/api/Donation/Expired";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<ICollection<UserDonationViewModel>>();
            return result;
        }

        public async Task<ICollection<UserDonationViewModel>> UserApprovedDonations(string token)
        {
            var apiUrl = "https://localhost:7005/api/Donation/Approved";
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<ICollection<UserDonationViewModel>>();
            return result;
        }

        public async Task<ICollection<UserDonationViewModel>> UserDisapprovedDonations( string token)
        {
            var apiUrl = "https://localhost:7005/api/Donation/Disapprove";
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<ICollection<UserDonationViewModel>>();
            return result;
        }

        public async Task<ICollection<UserDonationViewModel>> UserPendingDonations( string token)
        {
            var apiUrl = "https://localhost:7005/api/Donation/Pending";
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<ICollection<UserDonationViewModel>>();
            return result;
        }

        public async Task<ICollection<UserDonationViewModel>> UserReceivedDonations( string token)
        {
            var apiUrl = "https://localhost:7005/api/Donation/Received";
           
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<ICollection<UserDonationViewModel>>();
            return result;
        }

        public async Task<DonationTypeViewModel> ViewDonationType(string token)
        {
            var apiUrl = "https://localhost:7005/api/Donation/Count";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<DonationTypeViewModel>();
            return result;
        }

        public async Task<DonationTypeViewModel> DonationCount(string token)
        {
            var apiUrl = "https://localhost:7005/api/Donation/GetAllDOnationCount";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<DonationTypeViewModel>();
            return result;
        }


    }
}
