using Gateway.Models.Update;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Web.Pages
{
    public class ManagerProfileModel : PageModel
    {
        private readonly IUpdateProfileService _updateProfileService;
        public ManagerProfileModel(IUpdateProfileService updateProfileService)
        {
            _updateProfileService = updateProfileService;
        }

        [BindProperty]
        public ViewProfileViewModel Manager { get; set; } = new ViewProfileViewModel();
        public IActionResult OnGet()
        {
            if (TempData["ProfileData"] != null)
            {
                // Deserialize the data from TempData
                Manager = JsonConvert.DeserializeObject<ViewProfileViewModel>(TempData["ProfileData"].ToString());
            }
            return Page();
        }

        [BindProperty]
        public ManagerProfileUpdateModel UpdateModel { get; set; }
        public async Task<IActionResult> OnPost()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var update = await _updateProfileService.UpdateManagerProfileAsync(UpdateModel, token);
            if (update)
            {
                return RedirectToPage("/SuccessUpdate");
            }
            TempData["Message"] = "An Error OCCurred";
            return RedirectToPage("error");
        }
    }
}
