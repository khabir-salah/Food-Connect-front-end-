using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class ReceivedDonationsModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public ReceivedDonationsModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }

        [BindProperty]
        public ICollection<UserDonationViewModel> model { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var received = await _foodDonationService.UserReceivedDonations(token);
            model = received.Adapt<ICollection<UserDonationViewModel>>();
            return Page();
        }
    }
}
