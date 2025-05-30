using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.Booking;
using Infrastructure.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("/api[controller]")]
public class BookingController(IBookingService bookingService)
{
    [HttpGet]
    public async Task<Response<List<BookingDTO>>> GetAllAsync()
    {
        return await bookingService.GetAllAsync();
    }
    [HttpGet("{id}")]
    public async Task<Response<BookingDTO>> GetByIdAsync(int id)
    {
        return await bookingService.GetByIdAsync(id);
    }
    [HttpPut]
    public async Task<Response<string>> CreateAsync(BookingDTO userDTO)
    {
        return await bookingService.CreateAsync(userDTO);
    }
    [HttpPost]
    public async Task<Response<string>> UpdateAsync(int id, BookingDTO userDTO)
    {
        return await bookingService.UpdateAsync(id, userDTO);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await bookingService.DeleteAsync(id);
    }
}
