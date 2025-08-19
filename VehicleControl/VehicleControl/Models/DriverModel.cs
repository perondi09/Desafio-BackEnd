namespace VehicleControl.Models
{
    public enum CnhCategoryType
    {
        A,
        B,
        AB
    }

    public class DriverModel
    {
        public int Id { get; set; }
    public string? Name { get; set; }
    public string? Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Cnh { get; set; }
    public CnhCategoryType CnhCategory { get; set; }
    public string? CnhImage { get; set; }
    }
}