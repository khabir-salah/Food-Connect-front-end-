using Gateway.Extension;
using Gateway.Models.Donation;
using Gateway.Models.Registration;
using Gateway.Models.Update;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Services
{
    public class UpdateProfileService : IUpdateProfileService
    {
        private readonly HttpClient _httpClient;
        public UpdateProfileService()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromDays(7) 
            };

        }

        public async Task<bool> UpdateIndividualProfileAsync(IndividualProfileUpdateModel request, string token)
        {
            var apiUrl = $"{_httpClient.BaseAddress}api/Profile/UpdateProfile-Individual";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            return response.IsSuccessStatusCode? true : false;
        }

        public async Task<bool> UpdateOrganizationProfileAsync(OrganizationProfileUpdateModel request, string token)
        {
            var apiUrl = $"{_httpClient.BaseAddress}api/Profile/UpdateProfile-OrganizationHead";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            return response.IsSuccessStatusCode? true: false;
        }

        public async Task<bool> UpdateManagerProfileAsync(ManagerProfileUpdateModel request, string token)
        {
            var apiUrl = $"{_httpClient.BaseAddress}api/Profile/UpdateProfile-Manager";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> UpdateFamilyProfileAsync(FamilyUpdateProfileModel request, string token)
        {
            var apiUrl = $"{_httpClient.BaseAddress}api/Profile/UpdateProfile-FamilyHead";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<ViewProfileViewModel> ViewProfileAsync( string token)
        {
            var apiUrl = $"{_httpClient.BaseAddress}api/Profile/View-Profile";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<ViewProfileViewModel>();
            return result;
        }

        public async Task<PaginatedResponse<AllUsersViewModel>> ViewAllUserAsync(string token, int PageNumber = 1, int PageSize = 10)
        {
            var apiUrl = $"{_httpClient.BaseAddress}api/Profile/ViewAllUsers?PageNumber={PageNumber}&PageSize={PageSize}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(apiUrl);
            var result = await response.ReadContentAs<PaginatedResponse<AllUsersViewModel>>();
            return result;
        }

        public async Task<UserDeatilsModel> UserDetailAsync(Guid donationId)
        {
            var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}api/Profile/details/{donationId}", null);
            var result = await response.ReadContentAs<UserDeatilsModel>();
            return result;
        }

        public async Task<bool> ValidateUser(Guid userId)
        {
            var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}api/Profile/Validate/{userId}", null);
            return response.IsSuccessStatusCode ? true : false;
        }
    }
}
