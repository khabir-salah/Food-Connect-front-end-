using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace Web.Pages
{
    public class CreateDonationModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public CreateDonationModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateDonationModels model { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                var id = jwtToken?.Claims.First(claim => claim.Type == "Id").Value;
                Guid.TryParse(id, out Guid userId);

                model.UserId = userId;
            }
            if (ModelState.IsValid)
            {
                var send = await _foodDonationService.CreateDonation(model, token);
                if(send)
                {
                    TempData["Message"] = "Food Donation Created Successfully";
                    return RedirectToAction("/Success");
                }
                TempData["Message"] = "Food Donation Creation Failed";
                return Page();
            }
            TempData["Message"] = "Make Sure All Data Are Correct";
            return Page();
        }
    }
}
