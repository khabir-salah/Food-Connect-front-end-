using Gateway.Managers.Organisation;
using Gateway.Models.Organisation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IRegisterService _registerService;
        public LoginModel(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [BindProperty]
        public LoginViewModel AuthModel { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var response = await _registerService.Login(AuthModel);
                if(response.Token != null)
                {
                    HttpContext.Session.SetString("JWToken", response.Token.ToString());

                    return Redirect("/UserDashBoard");
                }
                TempData["Message"] = "Invalid Credential. Make sure you verify your Email address before Login";
                return Page();
            }
            TempData["Message"] = "Invalid Credential";
            return Page();
        }
    }
}
