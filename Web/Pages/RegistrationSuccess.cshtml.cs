using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class RegistrationSuccessModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
