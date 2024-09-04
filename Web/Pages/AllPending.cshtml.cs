using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AllPendingModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public AllPendingModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }

        [BindProperty]
        public PaginatedResponse<AllDonationViewModel> pending { get; set; } = new PaginatedResponse<AllDonationViewModel>();

        public async Task<IActionResult> OnGetAsync(int status, int PageNumber =1, int PageSize=10)
        {
            var token = HttpContext.Session.GetString("JWToken");

            pending = await _foodDonationService.AllDonations(new DonationsModel { status = 1}, token, PageNumber, PageSize);
            return Page();
        }

        [BindProperty]
        public Guid DonationId { get; set; }
        public async Task<IActionResult> OnPostApproveAsync()
        {
            var token = HttpContext.Session.GetString("JWToken");
            await _foodDonationService.ApproveByManager(DonationId, token);
            return RedirectToPage("/SuccessApprove");
        }


        [BindProperty]
        public DonationDisapproveModel DisapproveRequest { get; set; } = new DonationDisapproveModel();

        public async Task<IActionResult> OnPostDisapproveAsync()
        {
            var token = HttpContext.Session.GetString("JWToken");
            await _foodDonationService.DispproveByManager(DisapproveRequest, token);
            return RedirectToPage("/SuccessDisapprove");

        }


    }
}
