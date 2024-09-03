using Gateway.Managers.Organisation;
using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Web.Pages
{
    public class UserDashboardModel : PageModel
    {
        private readonly IFoodDonationService _foodService;
        public UserDashboardModel(IFoodDonationService foodService)
        {
            _foodService = foodService;
        }

        [BindProperty]
        public DonationTypeViewModel model { get; set; } = new DonationTypeViewModel();
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            model = await _foodService.DonationCount(token);
            var totalCount = model.PendingCount + model.ApproveCount + model.DisapproveCount + model.ReceivedCount + model.ClaimedCount + model.ExpiredCount;
            TempData["TotalCount"] = totalCount;


            return Page();
        }
    }
}
