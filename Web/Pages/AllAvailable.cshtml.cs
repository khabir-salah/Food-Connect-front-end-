using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AllAvailableModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public AllAvailableModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }
        public PaginatedResponse<AllDonationViewModel> Available { get; set; }

        public async Task<IActionResult> OnGetAsync(int status, int PageNumber = 1, int PageSize = 10)
        {
            var token = HttpContext.Session.GetString("JWToken");

            Available = await _foodDonationService.AllDonations(new DonationsModel { status = status }, token, PageNumber, PageSize);
            return Page();
        }
    }
}
