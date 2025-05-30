namespace Domain.DTOs.Car;

public class CarDTO
{
    public int Id { get; set; }
    public string Model { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; }
    public int BookingCount { get; set; }
}
