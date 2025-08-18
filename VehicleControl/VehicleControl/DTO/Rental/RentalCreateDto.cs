using VehicleControl.Models;

namespace VehicleControl.DTO.Vehicle.Rental
{
    public class RentalCreateDto
    {
    public int VehicleId { get; set; }
    public int DriverId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime ExpectedReturnDate { get; set; }
    public string Plan { get; set; }
    }
}