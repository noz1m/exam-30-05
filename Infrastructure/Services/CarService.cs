using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.Car;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class CarService(DataContext context) : ICarService
{
    public async Task<Response<List<CarDTO>>> GetAllAsync()
    {
        var car = await context.Cars
            .Include(c => c.Bookings)
            .Select(c => new CarDTO
            {
                Id = c.Id,
                Model = c.Model,
                PricePerDay = c.PricePerDay,
                IsAvailable = c.IsAvailable,
                BookingCount = c.Bookings.Count
            }).ToListAsync();
        return (car == null)
            ? new Response<List<CarDTO>>("Cars not found", HttpStatusCode.NotFound)
            : new Response<List<CarDTO>>(car, "Cars found");
    }
    public async Task<Response<CarDTO>> GetByIdAsync(int id)
    {
        var car = await context.Cars
            .Include(c => c.Bookings)
            .FirstOrDefaultAsync(c => c.Id == id);
        return car == null
            ? new Response<CarDTO>("Car not found", HttpStatusCode.NotFound)
            : new Response<CarDTO>(new CarDTO
            {
                Model = car.Model,
                PricePerDay = car.PricePerDay,
                IsAvailable = car.IsAvailable,
                BookingCount = car.Bookings.Count
            }, "Car found");
    }
    public async Task<Response<List<AvailableCarDto>>> GetAvailableCarDto()
    {
        var car = await context.Cars
            .Select(c => new CarDTO
            {
                Model = c.Model,
                PricePerDay = c.PricePerDay,
            }).ToListAsync();
        return car == null
            ? new Response<List<AvailableCarDto>>("Cars not found", HttpStatusCode.NotFound)
            : new Response<List<AvailableCarDto>>(null, "Cars found");
    }
    public async Task<Response<List<PopularCarDto>>> GetPopularCarDto()
    {
        var car = await context.Cars
           .Include(c => c.Bookings)
           .Select(c => new CarDTO
           {
               Model = c.Model,
               BookingCount = c.Bookings.Count
           }).ToListAsync();
        return car == null
            ? new Response<List<PopularCarDto>>("Cars not found", HttpStatusCode.NotFound)
            : new Response<List<PopularCarDto>>(null, "Cars found");
    }
    public async Task<Response<CarBookingDetailsDto>> GetCarBookingDetailsDto(int id)
    {
        var car = await context.Cars
            .Include(c => c.Users)
            .Include(c => c.Bookings)
            .FirstOrDefaultAsync(c => c.Id == id);
        return car == null
            ? new Response<CarBookingDetailsDto>("Car not found", HttpStatusCode.NotFound)
            : new Response<CarBookingDetailsDto>(new CarBookingDetailsDto
            {
                UserName = car.Users.UserName,
                StartDate = car.Bookings.StartDate,
                EndDate = car.Bookings.EndDate,
                TotalPrice = Bookings.TotalPrice                
            }, "Car found");
    }
    public async Task<Response<string>> CreateAsync(CarDTO carDTO)
    {
        var car = new Car
        {
            Model = carDTO.Model,
            PricePerDay = carDTO.PricePerDay,
            IsAvailable = carDTO.IsAvailable,
        };
        await context.Cars.AddAsync(car);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Car created", HttpStatusCode.Created)
            : new Response<string>("Car not created", HttpStatusCode.BadRequest);
    }
    public async Task<Response<string>> UpdateAsync(int id, CarDTO carDTO)
    {
        var carToUpdate = await context.Cars.FindAsync(id);
        if (carToUpdate == null)
            return new Response<string>("Car not found", HttpStatusCode.NotFound);
        carToUpdate.Model = carDTO.Model;
        carToUpdate.PricePerDay = carDTO.PricePerDay;
        carToUpdate.IsAvailable = carDTO.IsAvailable;
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Car updated", HttpStatusCode.OK)
            : new Response<string>("Car not updated", HttpStatusCode.BadRequest);
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var carToDelete = await context.Cars.FindAsync(id);
        if (carToDelete == null)
            return new Response<string>("Car not found", HttpStatusCode.NotFound);
        context.Cars.Remove(carToDelete);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Car deleted", HttpStatusCode.OK)
            : new Response<string>("Car not deleted", HttpStatusCode.BadRequest);
    }
}
