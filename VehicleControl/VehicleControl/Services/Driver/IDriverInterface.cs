using VehicleControl.DTO.Vehicle.Driver;
using VehicleControl.Models;

namespace VehicleControl.Services.Driver
{
    public interface IDriverInterface
    {
        Task<ResponseModel<DriverModel>> DriverCreate(DriverCreateDto driverCreateDto);
        Task<ResponseModel<DriverModel>> CnhUpdate(int id, DriverUpdateDto driverUpdateDto);
    }
}