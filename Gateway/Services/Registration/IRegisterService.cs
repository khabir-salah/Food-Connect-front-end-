using Gateway.Models.Organisation;
using Gateway.Models.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Managers.Organisation
{
    public interface IRegisterService
    {
        Task<bool> RegisterOrganisation(CreateOrganisationViewModel viewModel);
        Task<bool> RegisterIndividual(CreateIndividualViewModel viewModel);
        Task<bool> RegisterFamily(CreateFamilyViewModel viewModel);
        Task<bool> RegisterManager(CreateManagerViewModel viewModel);
        Task<LoginResponse> Login(LoginViewModel viewModel);
    }
}
