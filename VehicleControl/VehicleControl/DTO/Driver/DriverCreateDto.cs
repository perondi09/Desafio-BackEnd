namespace VehicleControl.DTO.Vehicle.Driver
{
    public enum CnhCategoryType
    {
        A,
        B,
        AB
    }
    public class DriverCreateDto
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cnh { get; set; }
        public string CnhCategory { get; set; }
        public CnhCategoryType CnhCategoryType { get; set; }
        public string CnhImage { get; set; }
    }
}