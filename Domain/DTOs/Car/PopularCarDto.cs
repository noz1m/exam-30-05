namespace Domain.DTOs.Car;

public class PopularCarDto : AvailableCarDto
{
    public List<BookingInfoDto> Bookings { get; set; }
}
