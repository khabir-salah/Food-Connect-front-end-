using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class ViewDonationCategoryModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public ViewDonationCategoryModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }

        [BindProperty]
        public DonationTypeViewModel model { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var category = await _foodDonationService.ViewDonationType(token);
             model = new DonationTypeViewModel
            {
                PendingCount = category.PendingCount,
                ApproveCount = category.ApproveCount,
                DisapproveCount = category.DisapproveCount,
                ReceivedCount = category.ReceivedCount,
                ExpiredCount = category.ExpiredCount,
            };

            return Page();
        }

    }
}
