using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    [MinLength(3)]
    public string UserName { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Email { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Phone { get; set; }

    public List<Car> Cars { get; set; }
    public List<Booking> Bookings { get; set; }
}
