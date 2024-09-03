using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class DispprovedDonationsModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public DispprovedDonationsModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }

        [BindProperty]
        public ICollection<UserDonationViewModel> model { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var disapproved = await _foodDonationService.UserDisapprovedDonations(token);
            model = disapproved.Adapt<ICollection<UserDonationViewModel>>();
            return Page();
        }
    }
}
