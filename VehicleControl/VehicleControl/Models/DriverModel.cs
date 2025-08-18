namespace VehicleControl.Models
{
    public class DriverModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cnh { get; set; }
        public string CnhCategory { get; set; }
        public string CnhImage { get; set; }      
    }
}