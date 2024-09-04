using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AllReceivedModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public AllReceivedModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }
        public PaginatedResponse<AllDonationViewModel> Received { get; set; }

        public async Task<IActionResult> OnGetAsync(int status, int PageNumber = 1, int PageSize = 10)
        {
            var token = HttpContext.Session.GetString("JWToken");

            Received = await _foodDonationService.AllDonations(new DonationsModel { status = status }, token, PageNumber, PageSize);
            return Page();
        }
    }
}
