using AutoMapper;
using BeyondComputersNi.Api.ViewModels.Authentication;
using BeyondComputersNi.Services.DataTransferObjects.Authentication;

namespace BeyondComputersNi.Api.MappingProfiles;

public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        CreateMap<AuthenticationDto, AuthenticationViewModel>();
        CreateMap<RefreshViewModel, RefreshDto>();
    }
}
