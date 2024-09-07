using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class RecipientClaimedDonationModel : PageModel
    {
       private readonly IFoodDonationService _foodDonationService;
        public RecipientClaimedDonationModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }

        //[BindProperty]
        public ICollection<UserDonationViewModel> claimed { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            claimed = await _foodDonationService.GetClaimedDonations(token);
            return Page();
        }
    }
}
