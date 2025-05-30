using Domain.ApiResponse;
using Domain.DTOs.User;
using Infrastructure.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("/api[controller]")]
public class UserController(IUserService userService)
{
    [HttpGet]
    public async Task<Response<List<UserDTO>>> GetAllAsync()
    {
        return await userService.GetAllAsync();
    }
    [HttpGet("{id}")]
    public async Task<Response<UserDTO>> GetByIdAsync(int id)
    {
        return await userService.GetByIdAsync(id);
    }
    [HttpPut]
    public async Task<Response<string>> CreateAsync(UserDTO userDTO)
    {
        return await userService.CreateAsync(userDTO);
    }
    [HttpPost]
    public async Task<Response<string>> UpdateAsync(int id, UserDTO userDTO)
    {
        return await userService.UpdateAsync(id, userDTO);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await userService.DeleteAsync(id);
    }
}
