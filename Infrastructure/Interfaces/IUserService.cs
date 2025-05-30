using Domain.ApiResponse;
using Domain.DTOs.User;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<List<UserDTO>>> GetAllAsync();
    Task<Response<UserDTO>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(UserDTO userDTO);
    Task<Response<List<FrequentRenterDto>>> GetFrequentRenterDto();
    Task<Response<string>> UpdateAsync(int id, UserDTO userDTO);
    Task<Response<string>> DeleteAsync(int id);
}
