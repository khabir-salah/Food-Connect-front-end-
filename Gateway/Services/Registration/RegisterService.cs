using Gateway.Extension;
using Gateway.Models.Organisation;
using Gateway.Models.Registration;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gateway.Managers.Organisation
{
    public class RegisterService : IRegisterService
    {
        private readonly HttpClient _httpClient;


        public RegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> Login(LoginViewModel viewModel)
        {
            string json = JsonSerializer.Serialize(viewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://localhost:7005/api/Identity/Login";
            var response = await _httpClient.PostAsync(url, content);

            if(response.IsSuccessStatusCode)
            {
                
                var resObject = await response.ReadContentAs<LoginResponse>();

                return new LoginResponse
                {
                    Message = resObject.Message,
                    Token = resObject.Token,
                };
            }
            return new LoginResponse
            {
                Message = "Failed",
                Token = null
            };
        }

        public async Task<bool> RegisterFamily(CreateFamilyViewModel viewModel)
        {
            string json = JsonSerializer.Serialize(viewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://localhost:7005/api/Identity/Register-Family";
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> RegisterIndividual(CreateIndividualViewModel viewModel)
        {
            string json = JsonSerializer.Serialize(viewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://localhost:7005/api/Identity/Register-Individual";
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> RegisterManager(CreateManagerViewModel viewModel)
        {
            string json = JsonSerializer.Serialize(viewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://localhost:7005/api/Identity/Register-Manager";
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> RegisterOrganisation(CreateOrganisationViewModel viewModel)
        {
            string json = JsonSerializer.Serialize(viewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://localhost:7005/api/Identity/Register-Organization";
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> ForgetPassword(string email)
        {
            string json = JsonSerializer.Serialize(email);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://localhost:7005/api/Identity/forgot-password";
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode ? true : false;
        }
    }
}
