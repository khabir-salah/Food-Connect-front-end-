using Gateway.Models.Message;
using Gateway.Services.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AllInappropriateReviewModel(IMessaging_Service _Service) : PageModel
    {
        [BindProperty]
        public ICollection<DonationReviewViewModel> reviewModel { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            reviewModel = await _Service.AllInappropriateReview(token);
            return Page();
        }
    }
}
