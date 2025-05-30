using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.User;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext context) : IUserService
{
    public async Task<Response<List<UserDTO>>> GetAllAsync()
    {
        var user = await context.Users
            .Include(u => u.Bookings)
            .Select(u => new UserDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                BookingCount = u.Bookings.Count
            }).ToListAsync();
        return (user == null)
            ? new Response<List<UserDTO>>("Users not found", HttpStatusCode.NotFound)
            : new Response<List<UserDTO>>(user, "Users found");
    }
    public async Task<Response<UserDTO>> GetByIdAsync(int id)
    {
        var user = await context.Users
            .Include(u => u.Bookings)
            .FirstOrDefaultAsync(u => u.Id == id);
        return user == null
            ? new Response<UserDTO>("User not found", HttpStatusCode.NotFound)
            : new Response<UserDTO>(new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                BookingCount = user.Bookings.Count
            }, "User found");
    }
    public async Task<Response<List<FrequentRenterDto>>> GetFrequentRenterDto()
    {
        var user = await context.Users
           .Include(u => u.Bookings)
           .Select(u => new UserDTO
           {
               UserName = u.UserName,
               BookingCount = u.Bookings.Count
           }).ToListAsync();
        return user == null
            ? new Response<List<FrequentRenterDto>>("Users not found", HttpStatusCode.NotFound)
            : new Response<List<FrequentRenterDto>>(null, "Users found");
    }
    public async Task<Response<string>> CreateAsync(UserDTO userDTO)
    {
        var user = new User
        {
            UserName = userDTO.UserName,
            Email = userDTO.Email,
        };
        await context.Users.AddAsync(user);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("User created", HttpStatusCode.Created)
            : new Response<string>("User not created", HttpStatusCode.BadRequest);
    }
    public async Task<Response<string>> UpdateAsync(int id, UserDTO userDTO)
    {
        var userToUpdate = await context.Users.FindAsync(id);
        if (userToUpdate == null)
            return new Response<string>("User not found", HttpStatusCode.NotFound);
        userToUpdate.UserName = userDTO.UserName;
        userToUpdate.Email = userDTO.Email;
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("User updated", HttpStatusCode.OK)
            : new Response<string>("User not updated", HttpStatusCode.BadRequest);
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var userToDelete = await context.Users.FindAsync(id);
        if (userToDelete == null)
            return new Response<string>("User not found", HttpStatusCode.NotFound);
        context.Users.Remove(userToDelete);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("User deleted", HttpStatusCode.OK)
            : new Response<string>("User not deleted", HttpStatusCode.BadRequest);
    }
}
