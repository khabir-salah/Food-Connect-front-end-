using Gateway.Extension;
using Gateway.Models.Donation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gateway.Services.FoodDonation
{
    public class FoodDonationService : IFoodDonationService
    {

        private readonly HttpClient _httpClient;
        public FoodDonationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
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
                content.Add(new StringContent(request.ExpirationDate.ToString("o")), nameof(request.ExpirationDate)); // "o" for ISO 8601 format
                content.Add(new StringContent(request.PickUpTime.ToString("o")), nameof(request.PickUpTime));
                content.Add(new StringContent(request.PickUpLocation), nameof(request.PickUpLocation));
                content.Add(new StringContent(request.UserId.ToString()), nameof(request.UserId));

                // Add primary image
                if (request.PrimaryImageUrl != null)
                {
                    var primaryImageContent = new StreamContent(request.PrimaryImageUrl.OpenReadStream());
                    primaryImageContent.Headers.ContentType = new MediaTypeHeaderValue(request.PrimaryImageUrl.ContentType);
                    content.Add(primaryImageContent, nameof(request.PrimaryImageUrl), request.PrimaryImageUrl.FileName);
                }

                // Add additional images
                foreach (var file in request.DonationImages)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    content.Add(fileContent, nameof(request.DonationImages), file.FileName);
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsync(url, content);
                return response.IsSuccessStatusCode ? true : false;
            }
            

        }

        public async Task<UserDonationViewModel> GetClaimedDonations()
        {
            var apiUrl = "https://localhost:7005/api/Donation/claimed";
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<UserDonationViewModel>();
            return result;
        }

        public async Task<UserDonationViewModel> ReceiveFoodDonation()
        {
            var apiUrl = "https://localhost:7005/api/Donation/All";
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<UserDonationViewModel>();
            return result;
        }

        public async Task<DonationResponseCommandModel> SearchDonations(string location, int? minQuantity, int? maxQuantity)
        {
            var queryString = $"?location={location}&minQuantity={minQuantity}&maxQuantity={maxQuantity}";
            var apiUrl = $"https://localhost:7005/api/Donation/Search{queryString}";
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<DonationResponseCommandModel>();
            return result;
        }

        public async Task<bool> SendMessage(SendDonationMessageModel model)
        {
            var messageRequest = new SendDonationMessageRequest
            {
                SenderId = model.SenderId,
                RecipientId = model.RecipientId,
                DonationId = model.DonationId,
                Content = model.Content
            };

            var response = await _httpClient.PostAsJsonAsync("api/Message", messageRequest);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<TrackDonationViewModel> TrackDonation(Guid donationId)
        {
            var response = await _httpClient.GetAsync($"api/Donation/{donationId}");
            var result = await response.ReadContentAs<TrackDonationViewModel>();
            return result;
        }

        public async Task<UserDonationViewModel> UserApprovedDonations()
        {
            var apiUrl = "https://localhost:7005/api/Donation/Approved";
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<UserDonationViewModel>();
            return result;
        }

        public async Task<UserDonationViewModel> UserDisapprovedDonations()
        {
            var apiUrl = "https://localhost:7005/api/Donation/Disapprove";
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<UserDonationViewModel>();
            return result;
        }

        public async Task<UserDonationViewModel> UserPendingDonations( string token)
        {
            var apiUrl = "https://localhost:7005/api/Donation/Pending";
            var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(10) // Set to 5 minutes or any desired value
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<UserDonationViewModel>();
            return result;
        }

        public async Task<UserDonationViewModel> UserReceivedDonations()
        {
            var apiUrl = "https://localhost:7005/api/Donation/Received";
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<UserDonationViewModel>();
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
    }
}
