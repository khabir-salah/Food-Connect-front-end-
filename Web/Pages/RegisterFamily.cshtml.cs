using Gateway.Managers.Organisation;
using Gateway.Models.Registration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class RegisterFamilyModel : PageModel
    {
        private readonly IRegisterService _registerService;
        public RegisterFamilyModel(IRegisterService registerService)
        {
            _registerService = registerService;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateFamilyViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var send = await _registerService.RegisterFamily(ViewModel);
                if (send)
                {
                    return RedirectToAction("/login");
                }
                TempData["Message"] = "Registration Failed. Make Sure All Data Are Correct";
                return Page();
            }
            TempData["Message"] = "Invalid Credential";

            return Page();
        }
    }
}
