using Gateway.Models.Donation;
using Gateway.Services.FoodDonation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class SearchModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        
    }
}
