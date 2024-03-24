using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.Interfaces;

public interface IUserService
{
    Task<bool> UserExistsAsync(string email);
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserAsync(string email);
    bool PasswordIsCorrect(UserDto user, string passwordAttempt);
    Task<bool> AddUserAsync(UserDto userDto);
    Task DeleteUserAsync(UserDto userDto);
}
