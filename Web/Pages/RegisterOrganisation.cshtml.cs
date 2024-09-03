using Gateway.Managers.Organisation;
using Gateway.Models.Organisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class RegisterOrganisationModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRegisterService _organisationManager;

        public RegisterOrganisationModel(ILogger<IndexModel> logger, IRegisterService organisationManager)
        {
            _logger = logger;
            _organisationManager = organisationManager;
        }
        [BindProperty]
        public CreateOrganisationViewModel ViewModel { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var send = await _organisationManager.RegisterOrganisation(ViewModel);
                if(send)
                {
                    return RedirectToPage("/RegistrationSuccess");
                }
                TempData["Message"] = "Registration Failed. Make Sure All Data Are Correct";
                return Page();
            }
            TempData["Message"] = "Invalid Credential";
           
            return Page();
        }
    }
}
