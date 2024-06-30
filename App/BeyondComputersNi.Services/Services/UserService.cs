using AutoMapper;
using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using BeyondComputersNi.Services.DataTransferObjects;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace BeyondComputersNi.Services.Services;

public class UserService(IRepository<User> userRepo, IMapper mapper) : IUserService
{
    public Task<bool> UserExistsAsync(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return Task.FromResult(false);

        return userRepo.Get().Where(u => u.Email == email).AnyAsync();
    }

    public Task<List<UserDto>> GetAllUsersAsync()
    {
        return mapper.ProjectTo<UserDto>(userRepo.Get())
            .ToListAsync();
    }

    public Task<UserDto?> GetUserAsync(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return Task.FromResult<UserDto?>(null);

        return mapper.ProjectTo<UserDto>(userRepo.Get())
            .SingleOrDefaultAsync(u => u.Email == email);
    }

    public bool PasswordIsCorrect(UserDto? user, string? passwordAttempt)
    {
        if (user == null || string.IsNullOrEmpty(passwordAttempt)) return false;

        return BC.Verify(passwordAttempt, user.Password);
    }

    public async Task<bool> AddUserAsync(UserDto userDto)
    {
        var user = mapper.Map<User>(userDto);
        await userRepo.AddAsync(user);

        var success = await userRepo.SaveChangesAsync() > 0;

        return success;
    }

    public Task DeleteUserAsync(UserDto userDto)
    {
        throw new NotImplementedException();
    }
}
