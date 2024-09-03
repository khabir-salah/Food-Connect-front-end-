using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class ApprovedDonationsModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public ApprovedDonationsModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }

        [BindProperty]
        public ICollection<UserDonationViewModel> model { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var approved = await _foodDonationService.UserApprovedDonations(token);
            model = approved.Adapt<ICollection<UserDonationViewModel>>();
            return Page();
        }
    }
}
