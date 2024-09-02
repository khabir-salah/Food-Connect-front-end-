using Gateway.Models.Donation;
using Gateway.Models.Registration;
using Gateway.Models.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Services
{
    public interface IUpdateProfileService
    {
        Task<bool> UpdateIndividualProfileAsync(IndividualProfileUpdateModel request, string token);
        Task<bool> UpdateOrganizationProfileAsync(OrganizationProfileUpdateModel request, string token);
        Task<bool> UpdateManagerProfileAsync(ManagerProfileUpdateModel request, string token);
        Task<bool> UpdateFamilyProfileAsync(FamilyUpdateProfileModel request, string token);
        Task<ViewProfileViewModel> ViewProfileAsync(string token);
        Task<PaginatedResponse<AllUsersViewModel>> ViewAllUserAsync(string token, int PageNumber = 1, int PageSize = 10);
        Task<UserDeatilsModel> UserDetailAsync(Guid donationId);
        Task<bool> ValidateUser(Guid userId);
    }
}
