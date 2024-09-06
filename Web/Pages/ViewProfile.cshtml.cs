using Domain.Constant;
using Gateway.Models.Update;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace Web.Pages
{
    public class ViewProfileModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUpdateProfileService _updateProfileService;

        public ViewProfileModel(IHttpContextAccessor httpContextAccessor, IUpdateProfileService updateProfileService)
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(20) 
            };
            _httpContextAccessor = httpContextAccessor;
            _updateProfileService = updateProfileService;
        }

        public ViewProfileViewModel profileData { get; set; } = new ViewProfileViewModel();
        public async Task<IActionResult> OnGet()
        {

            var token = HttpContext.Session.GetString("JWToken");
            

            profileData = await _updateProfileService.ViewProfileAsync(token);
            TempData["ProfileData"] = JsonConvert.SerializeObject(profileData);


            //view to return based on the user's role
            switch (profileData.Role)
            {
                case RoleConst.Individual:
                    return RedirectToPage("IndividualProfile");

                case RoleConst.OrganizationHead:
                    return RedirectToPage("OrganizationProfile");

                case RoleConst.FamilyHead:
                    return RedirectToPage("FamilyHeadProfile");

                case RoleConst.Manager:
                    return RedirectToPage("ManagerProfile");

                default:
                    return RedirectToAction("Error");
            }

        }
    }
}
