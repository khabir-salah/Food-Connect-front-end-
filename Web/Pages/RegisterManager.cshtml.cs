using Gateway.Managers.Organisation;
using Gateway.Models.Registration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class RegisterManagerModel : PageModel
    {
        private readonly IRegisterService _registerService;
        public RegisterManagerModel(IRegisterService registerService)
        {
            _registerService = registerService;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateManagerViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var send = await _registerService.RegisterManager(ViewModel);
                if (send)
                {
                    return RedirectToAction("/AdminDashBoard");
                }
                TempData["Message"] = "Registration Failed. Make Sure All Data Are Correct";
                return Page();
            }
            TempData["Message"] = "Invalid Credential";

            return Page();
        }
    }
}
