using Microsoft.EntityFrameworkCore;
using VehicleControl.Data;
using VehicleControl.DTO.Vehicle;
using VehicleControl.Models;

namespace VehicleControl.Services.Vehicle
{
    public class VehicleService : IVehicleInterface
    {
        private readonly AppDbContext _context;

        public VehicleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<VehicleModel>> CreateVehicle(VehicleCreateDto vehicleCreateDto)
        {
            ResponseModel<VehicleModel> response = new ResponseModel<VehicleModel>();

            try
            {
                var vehicle = new VehicleModel
                {
                    Indentifier = vehicleCreateDto.Indentifier,
                    Year = vehicleCreateDto.Year,
                    Model = vehicleCreateDto.Model,
                    Plate = vehicleCreateDto.Plate
                };

                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                response.Data = vehicle;
                response.Status = true;
                response.Message = "Vehicle created successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<VehicleModel>> GetVehicleByPlate(string Plate)
        {
            ResponseModel<VehicleModel> response = new ResponseModel<VehicleModel>();
            try
            {

                var vehicle = await _context.Vehicles.FirstOrDefaultAsync(vehicleDb => vehicleDb.Plate == Plate);

                if (vehicle == null)
                {
                    response.Message = "Vehicle not found";
                    response.Status = false;
                    return response;
                }

                response.Data = vehicle;
                response.Status = true;
                response.Message = "Vehicle found successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<VehicleModel>> UpdateVehicle(int idVehicle, VehicleUpdateDto vehicleUpdateDto)
        {
            ResponseModel<VehicleModel> response = new ResponseModel<VehicleModel>();

            try
            {
                var vehicle = await _context.Vehicles.FindAsync(idVehicle);
                if (vehicle == null)
                {
                    response.Message = "Vehicle not found";
                    response.Status = false;
                    return response;
                }

                vehicle.Plate = vehicleUpdateDto.Plate;
                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();

                response.Data = vehicle;
                response.Status = true;
                response.Message = "Vehicle updated successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<VehicleModel>> GetVehicleById(int idVehicle)
        {
            ResponseModel<VehicleModel> response = new ResponseModel<VehicleModel>();
            try
            {
                var vehicle = await _context.Vehicles.FirstOrDefaultAsync(vehicleDb => vehicleDb.Id == idVehicle);

                if (vehicle == null)
                {
                    response.Message = "Vehicle not found";
                    response.Status = false;
                    return response;
                }

                response.Data = vehicle;
                response.Status = true;
                response.Message = "Vehicle found successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<VehicleModel>> DeleteVehicle(int idVehicle)
        {
            ResponseModel<VehicleModel> response = new ResponseModel<VehicleModel>();

            try
            {
                var vehicle = await _context.Vehicles.FindAsync(idVehicle);
                if (vehicle == null)
                {
                    response.Message = "Vehicle not found";
                    response.Status = false;
                    return response;
                }

                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
                response.Data = vehicle;
                response.Status = true;
                response.Message = "Vehicle deleted successfully";

                return response;
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }   
}
