using Domain.ApiResponse;
using Domain.DTOs.Car;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ICarService
{
    Task<Response<List<CarDTO>>> GetAllAsync();
    Task<Response<CarDTO>> GetByIdAsync(int id);
    Task<Response<List<AvailableCarDto>>> GetAvailableCarDto();
    Task<Response<List<PopularCarDto>>> GetPopularCarDto();
    Task<Response<CarBookingDetailsDto>> GetCarBookingDetailsDto(int id);
    Task<Response<string>> CreateAsync(CarDTO carDTO);
    Task<Response<string>> UpdateAsync(int id,CarDTO carDTO);
    Task<Response<string>> DeleteAsync(int id);
}
