using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class PendingDonationsModel : PageModel
    {
        private readonly IFoodDonationService _foodDonationService;
        public PendingDonationsModel(IFoodDonationService foodDonationService)
        {
            _foodDonationService = foodDonationService;
        }
        public void OnGet()
        {
        }
    }
}
