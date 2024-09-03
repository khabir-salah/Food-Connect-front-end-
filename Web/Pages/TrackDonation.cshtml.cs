using Gateway.Models.Donation;
using Gateway.Models.Message;
using Gateway.Services.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace Web.Pages
{
    public class TrackDonationModel : PageModel
    {
        private readonly IMessaging_Service _message;
        public TrackDonationModel(IMessaging_Service message)
        {
            _message = message;
        }

        [BindProperty]
        public DonationWithMessagesView viewPage { get; set; } = new DonationWithMessagesView();
        public async Task<IActionResult> OnGet(Guid DonationId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            var id = jwtToken?.Claims.First(claim => claim.Type == "Id").Value;
            Guid.TryParse(id, out Guid userId);

            viewPage = await _message.GetDonationWithMessages(DonationId, token);
            viewPage.IsDonor = viewPage.Donation.UserId == userId;
            TempData["DonationId"] = DonationId;
            return Page();
        }

        [BindProperty]
        public MessageModel receive {  get; set; } = new MessageModel();
        public async Task<IActionResult> OnPostMarkDonationAsReceivedAsync()
        {
            receive.DonationId = (Guid)TempData["DonationId"];
            var token = HttpContext.Session.GetString("JWToken");
            var received = await _message.MarkDonationAsReceived(receive, token);
            if(received)
            {
                return RedirectToPage("/SuccessReceived");
            }
            TempData["Message"] = "Donor Already Changed Status Donation";
            return Page();
        }

        [BindProperty]
        public MessageModelAvailable available { get; set; }
        public async Task<IActionResult> OnPostMarkDonationAsAvailabledAsync()
        {
            available.DonationId = (Guid)TempData["DonationId"];
            var token = HttpContext.Session.GetString("JWToken");
            var changeStatus = await _message.MarkAvailable(available, token);
            if(changeStatus)
            {
                return RedirectToPage("/SuccessAvailable");
            }
            TempData["Message"] = "Receipient Already Changed Status Donation";
            return Page();
        }
    }
}
