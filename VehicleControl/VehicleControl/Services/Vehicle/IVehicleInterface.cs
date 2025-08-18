using VehicleControl.DTO.Vehicle;
using VehicleControl.Models;

namespace VehicleControl.Services.Vehicle
{
    public interface IVehicleInterface
    {
        Task<ResponseModel<VehicleModel>> GetVehicleByPlate(string Plate);
        Task<ResponseModel<VehicleModel>> CreateVehicle(VehicleCreateDto vehicleCreateDto);
        Task<ResponseModel<VehicleModel>> UpdateVehicle(int idVehicle, VehicleUpdateDto vehicleUpdateDto);
        Task<ResponseModel<VehicleModel>> DeleteVehicle(int idVehicle);
        Task<ResponseModel<VehicleModel>> GetVehicleById(int idVehicle);
    }
}
