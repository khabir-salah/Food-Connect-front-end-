using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class SearchResultModel(IFoodDonationService _foodDonation) : PageModel
    {
        [BindProperty]
        public PaginatedResponse<UserDonationViewModel> pending { get; set; }
        public async Task<IActionResult> OnGet(string location, int? minQuantity, int? maxQuantity)
        {
            var token = HttpContext.Session.GetString("JWToken");
            pending = await _foodDonation.SearchDonations(token, location, minQuantity, maxQuantity);
            return Page();
        }
    }
}