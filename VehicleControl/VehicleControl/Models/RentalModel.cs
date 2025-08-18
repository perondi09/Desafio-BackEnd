namespace VehicleControl.Models
{
    public class RentalModel
    {
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public VehicleModel Vehicle { get; set; }
    public int DriverId { get; set; }
    public DriverModel Driver { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime ExpectedReturnDate { get; set; }
    public decimal Price { get; set; }
    public string Plan { get; set; }
    }
}