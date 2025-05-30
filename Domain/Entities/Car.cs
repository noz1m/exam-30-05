using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Car
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Model { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; }

    public User Users { get; set; }
    public List<Booking> Bookings { get; set; }
}
