using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.Interfaces;

public interface IUserService
{
    Task<bool> UserExistsAsync(string email);
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserAsync(string email);
    Task<bool> AddUserAsync(UserDto userDto);
    Task DeleteUserAsync(UserDto userDto);
}
