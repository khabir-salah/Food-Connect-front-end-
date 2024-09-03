using Gateway.Managers.Organisation;
using Gateway.Models.Organisation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Domain.Constant;
using Gateway.Services.FoodDonation;
using Gateway.Models.Donation;

namespace Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IRegisterService _registerService;
        private readonly IFoodDonationService _foodService;
        public LoginModel(IRegisterService registerService, IFoodDonationService foodService)
        {
            _registerService = registerService;
            _foodService = foodService;
        }

        [BindProperty]
        public LoginViewModel AuthModel { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DonationTypeViewModel model { get; set; } = new DonationTypeViewModel();
        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var response = await _registerService.Login(AuthModel);
                if(response.Token != null)
                {
                    HttpContext.Session.SetString("JWToken", response.Token.ToString());
                    var token = HttpContext.Session.GetString("JWToken");
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                    var role = jwtToken?.Claims.First(claim => claim.Type == "Roles").Value;
                   
                    if(role == RoleConst.Manager)
                    {
                        return Redirect("/ManagerDashBoard");
                    }
                    else if(role == RoleConst.Admin)
                    {
                        model = await _foodService.DonationCount(response.Token);
                        TempData["PendingCount"] = model.PendingCount;
                        TempData["ApproveCount"] = model.ApproveCount;
                        TempData["DisapproveCount"] = model.DisapproveCount;
                        TempData["ReceivedCount"] = model.ReceivedCount;
                        TempData["ClaimedCount"] = model.ClaimedCount;
                        TempData["ExpiredCount"] = model.ExpiredCount;
                        return Redirect("/AdminDashBoard");
                    }
                    else
                    {
                        return Redirect("/UserDashboard");
                    }
                    
                }
                TempData["Message"] = "Invalid Credential. Make sure you verify your Email address before Login";
                return Page();
            }
            TempData["Message"] = "Invalid Credential";
            return Page();
        }
    }
}
