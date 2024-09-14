using Gateway.Extension;
using Gateway.Models.Donation;
using Gateway.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gateway.Services.Messaging
{
    public class MessagingService : IMessaging_Service
    {
        private readonly HttpClient _httpClient;
        public MessagingService()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromDays(7) 
            };

        }
        public async Task<bool> MarkAvailable(MessageModelAvailable request, string token)
        {
            string json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = $"{_httpClient.BaseAddress}api/Message/Donation-Available";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> MarkDonationAsReceived(MessageModel request, string token)
        {
            var url = $"{_httpClient.BaseAddress}api/Message/mark-received";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode ? true : false;
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

     

        public async Task<DonationWithMessagesView> GetDonationWithMessages(Guid donationId, string token)
        {
            var url = $"{_httpClient.BaseAddress}api/Message/{donationId}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(url);
            var result = await response.ReadContentAs<DonationWithMessagesView>();
            return result;
        }
        

        public async Task<ICollection<DonationReviewViewModel>> Review( string token)
        {
            var url = $"{_httpClient.BaseAddress}api/Message/Reviews";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(url);
            var result = await response.ReadContentAs<ICollection<DonationReviewViewModel>>();
            return result;
        }

        public async Task<ICollection<DonationReviewViewModel>> AllInappropriateReview(string token)
        {
            var url = $"{_httpClient.BaseAddress}api/Message/AllReviews";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(url);
            var result = await response.ReadContentAs<ICollection<DonationReviewViewModel>>();
            return result;
        }

    }
}
