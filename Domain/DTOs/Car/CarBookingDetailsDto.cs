namespace Domain.DTOs.Car;

public class CarBookingDetailsDto : CarDTO
{
    public string UserName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
}
