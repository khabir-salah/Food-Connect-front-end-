using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class UserExpiredDonationModel(IFoodDonationService _foodDonationService) : PageModel
    {
        [BindProperty]
        public ICollection<UserDonationViewModel> model { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var approved = await _foodDonationService.UserExpiredDonations(token);
            model = approved.Adapt<ICollection<UserDonationViewModel>>();
            return Page();
        }
    }
}
