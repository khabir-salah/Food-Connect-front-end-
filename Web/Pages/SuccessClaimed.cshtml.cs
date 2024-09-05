using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class SuccessClaimedModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
