using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.Booking;
using Domain.DTOs.User;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class BookingService(DataContext context) : IBookingService
{
    public async Task<Response<List<BookingDTO>>> GetAllAsync()
    {
        var booking = await context.Bookings
            .Include(b => b.Users)
            .Include(b => b.Cars)
            .Select(b => new BookingDTO
            {
                Id = b.Id,
                UserId = b.UserId,
                CarId = b.CarId,
                CarModel = b.Cars.Model,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                TotalPrice = b.TotalPrice
            }).ToListAsync();
        return (booking == null)
            ? new Response<List<BookingDTO>>("Bookings not found", HttpStatusCode.NotFound)
            : new Response<List<BookingDTO>>(booking, "Bookings found");
    }
    public async Task<Response<BookingDTO>> GetByIdAsync(int id)
    {
        var booking = await context.Bookings
            .Include(b => b.Users)
            .Include(b => b.Cars)
            .FirstOrDefaultAsync(b => b.Id == id);
        return booking == null
            ? new Response<BookingDTO>("Booking not found", HttpStatusCode.NotFound)
            : new Response<BookingDTO>(new BookingDTO
            {
                Id = booking.Id,
                UserId = booking.UserId,
                CarId = booking.CarId,
                CarModel = booking.Cars.Model,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                TotalPrice = booking.TotalPrice
            }, "Booking found");
    }
    public async Task<Response<string>> CreateAsync(BookingDTO bookingDTO)
    {
        var booking = new Booking
        {
            UserId = bookingDTO.UserId,
            CarId = bookingDTO.CarId,
            StartDate = bookingDTO.StartDate,
            EndDate = bookingDTO.EndDate,
            TotalPrice = bookingDTO.TotalPrice
        };
        await context.Bookings.AddAsync(booking);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Booking created", HttpStatusCode.Created)
            : new Response<string>("Booking not created", HttpStatusCode.BadRequest);
    }
    public async Task<Response<string>> UpdateAsync(int id, BookingDTO bookingDTO)
    {
        var bookingToUpdate = await context.Bookings.FindAsync(id);
        if (bookingToUpdate == null)
            return new Response<string>("Booking not found", HttpStatusCode.NotFound);
        bookingToUpdate.UserId = bookingDTO.UserId;
        bookingToUpdate.CarId = bookingDTO.CarId;
        bookingToUpdate.StartDate = bookingDTO.StartDate;
        bookingToUpdate.EndDate = bookingDTO.EndDate;
        bookingToUpdate.TotalPrice = bookingDTO.TotalPrice;
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Booking updated", HttpStatusCode.OK)
            : new Response<string>("Booking not updated", HttpStatusCode.BadRequest);
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var bookingToDelete = await context.Bookings.FindAsync(id);
        if (bookingToDelete == null)
            return new Response<string>("Booking not found", HttpStatusCode.NotFound);
        context.Bookings.Remove(bookingToDelete);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Booking deleted", HttpStatusCode.OK)
            : new Response<string>("Booking not deleted", HttpStatusCode.BadRequest);
    }
}
