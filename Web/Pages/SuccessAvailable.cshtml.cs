using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class SuccessAvailableModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
