using Gateway.Models.Update;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Web.Pages
{
    public class FamilyHeadProfileModel : PageModel
    {
        private readonly IUpdateProfileService _updateProfileService;
        public FamilyHeadProfileModel(IUpdateProfileService updateProfileService)
        {
            _updateProfileService = updateProfileService;
        }

        [BindProperty]
        public ViewProfileViewModel model { get; set; } = new ViewProfileViewModel();
        public IActionResult OnGet()
        {
            if (TempData["ProfileData"] != null)
            {
                // Deserialize the data from TempData
                model = JsonConvert.DeserializeObject<ViewProfileViewModel>(TempData["ProfileData"].ToString());
            }
            return Page();
        }

        [BindProperty]
        public FamilyUpdateProfileModel UpdateModel { get; set; }
        public async Task<IActionResult> OnPost()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var update = await _updateProfileService.UpdateFamilyProfileAsync(UpdateModel, token);
            if (update)
            {
                return RedirectToPage("/SuccessUpdate");
            }
            TempData["Message"] = "An Error Occured. Profile Not Updated";
            return RedirectToAction("Error");
        }
    }
}
