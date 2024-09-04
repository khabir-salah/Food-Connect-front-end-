using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AllClaimedModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public AllClaimedModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }
        public PaginatedResponse<AllDonationViewModel> Claimed { get; set; }

        public async Task<IActionResult> OnGetAsync(int status, int PageNumber = 1, int PageSize = 10)
        {
            var token = HttpContext.Session.GetString("JWToken");

            Claimed = await _foodDonationService.AllDonations(new DonationsModel { status = status }, token, PageNumber, PageSize);
            //pending.Adapt<PaginatedResponse<AllDonationViewModel>>();
            return Page();
        }
    }
}
