using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AllApprovedModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public AllApprovedModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }
        public PaginatedResponse<AllDonationViewModel> Approved { get; set; }

        public async Task<IActionResult> OnGetAsync(int status, int PageNumber = 1, int PageSize = 10)
        {
            var token = HttpContext.Session.GetString("JWToken");

            Approved = await _foodDonationService.AllDonations(new DonationsModel { status = status }, token, PageNumber, PageSize);
            return Page();
        }
    }
}
