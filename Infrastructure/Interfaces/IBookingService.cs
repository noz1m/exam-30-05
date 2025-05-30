using Domain.ApiResponse;
using Domain.DTOs.Booking;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IBookingService
{
    Task<Response<List<BookingDTO>>> GetAllAsync();
    Task<Response<BookingDTO>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(BookingDTO bookingDTO);
    Task<Response<string>> UpdateAsync(int id,BookingDTO bookingDTO);
    Task<Response<string>> DeleteAsync(int id);
}
