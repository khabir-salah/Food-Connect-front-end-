using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class ReceiveFoodDonationModel(IFoodDonationService _foodDonationService) : PageModel
    {
        [BindProperty]
        public PaginatedResponse<UserDonationViewModel> pending { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            pending = await _foodDonationService.ReceivableFoodDonation(token);

            return Page();
        }

        [BindProperty]
        public Guid Id { get; set; }
        public async Task<IActionResult> OnPostClaimAsync()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var claim = await _foodDonationService.ClaimDonation(Id, token);
            if(!claim)
            {
                TempData["Message"] = "You can Only Claim A Donation After 5hrs Of First Claim";
                return Page();
            }
            return RedirectToPage("/SuccessClaimed");
        }
    }
}
