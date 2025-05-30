using Domain.ApiResponse;
using Domain.DTOs.Car;
using Infrastructure.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("/api[controller]")]
public class CarController(ICarService carService)
{
    [HttpGet]
    public async Task<Response<List<CarDTO>>> GetAllAsync()
    {
        return await carService.GetAllAsync();
    }
    [HttpGet("{id}")]
    public async Task<Response<CarDTO>> GetByIdAsync(int id)
    {
        return await carService.GetByIdAsync(id);
    }
    [HttpGet("available-now")]
    public async Task<Response<List<AvailableCarDto>>> GetAvailableCarDto()
    {
        return await carService.GetAvailableCarDto();
    }
    [HttpPut]
    public async Task<Response<string>> CreateAsync(CarDTO userDTO)
    {
        return await carService.CreateAsync(userDTO);
    }
    [HttpPost]
    public async Task<Response<string>> UpdateAsync(int id, CarDTO userDTO)
    {
        return await carService.UpdateAsync(id, userDTO);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await carService.DeleteAsync(id);
    }
}
