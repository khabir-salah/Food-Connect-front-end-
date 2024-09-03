using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class PendingDonationsModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public PendingDonationsModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }

        [BindProperty]
        public ICollection<UserDonationViewModel> model { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var pendingDonation = await _foodDonationService.UserPendingDonations(token);
            model = pendingDonation.Adapt<ICollection<UserDonationViewModel>>();
            return Page();
        }
    }
}
