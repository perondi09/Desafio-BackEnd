namespace VehicleControl.DTO.Vehicle.Driver
{
    public class DriverCreateDto
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cnh { get; set; }
        public string CnhCategory { get; set; }
        public string CnhImage { get; set; }
    }
}