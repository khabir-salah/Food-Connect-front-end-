using Gateway.Models.Update;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class FamilyHeadDetailsModel : PageModel
    {
        private readonly IUpdateProfileService _updateProfileService;
        public FamilyHeadDetailsModel(IUpdateProfileService updateProfileService)
        {
            _updateProfileService = updateProfileService;
        }

        [BindProperty]
        public UserDeatilsModel detail { get; set; }
        public async Task<IActionResult> OnGet(Guid UserId)
        {
            detail = await _updateProfileService.UserDetailAsync(UserId);
            TempData["Activate"] = detail.IsActivated ? true : false;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid userId)
        {
             await _updateProfileService.ValidateUser(userId);
            return RedirectToPage("/SuccessValidate");
        }
    }
}
