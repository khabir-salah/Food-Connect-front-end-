using Domain.Constant;
using Gateway.Models.Donation;
using Gateway.Models.Registration;
using Gateway.Models.Update;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace Web.Pages
{
    public class ViewAllUserModel : PageModel
    {
        private readonly IUpdateProfileService _updateProfileService;
        public ViewAllUserModel(IUpdateProfileService updateProfileService)
        {
            _updateProfileService = updateProfileService;
        }

        public PaginatedResponse<AllUsersViewModel> model { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWToken");
            model = await _updateProfileService.ViewAllUserAsync(token);
            return Page();
        }

    }
}
