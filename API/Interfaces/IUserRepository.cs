using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUsernameAsync(string username);
    Task<IEnumerable<AppUser>> GetUsersAsync();
    void Update(AppUser user);
    Task<bool> SaveAllAsync();

    Task<MemberDto?> GetMemberByUsernameAsync(string username);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
}
