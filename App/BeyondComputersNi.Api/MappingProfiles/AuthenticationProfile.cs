using AutoMapper;
using BeyondComputersNi.Services.DataTransferObjects.Authentication;
using BeyondComputersNi.Shared.ViewModels.Authentication;

namespace BeyondComputersNi.Api.MappingProfiles;

public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        CreateMap<AuthenticationDto, AuthenticationViewModel>();
        CreateMap<RefreshViewModel, RefreshDto>();
    }
}
