using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUserByUsernameAsync(string username)
    {
        var user = await userRepository.GetMemberByUsernameAsync(username);
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsersAsync()
    {
        var users = await userRepository.GetMembersAsync();
        return Ok(users);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (username == null)
            return BadRequest("No isername found in token");

        var user = await userRepository.GetUserByUsernameAsync(username);
        if (user == null)
            return BadRequest("Counld not  find user");

        mapper.Map(memberUpdateDto, user);

        if (await userRepository.SaveAllAsync())
            return NoContent();

        return BadRequest("Failed to update the user");
    }
}
