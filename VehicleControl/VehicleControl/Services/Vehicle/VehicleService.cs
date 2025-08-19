using Microsoft.EntityFrameworkCore;
using VehicleControl.Data;
using VehicleControl.DTO.Vehicle;
using VehicleControl.Models;

namespace VehicleControl.Services.Vehicle
{
    public class VehicleService : IVehicleInterface
    {
        private readonly AppDbContext _context;
        private readonly ILogger<VehicleService> _logger;

        public VehicleService(AppDbContext context, ILogger<VehicleService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseModel<VehicleModel>> CreateVehicle(VehicleCreateDto vehicleCreateDto)
        {
            ResponseModel<VehicleModel> response = new ResponseModel<VehicleModel>();

            try
            {
                _logger.LogInformation("Attempting to create vehicle with plate {Plate}", vehicleCreateDto.Plate);
                var existingVehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Plate == vehicleCreateDto.Plate);
                if (existingVehicle != null)
                {
                    _logger.LogWarning("Plate {Plate} already exists", vehicleCreateDto.Plate);
                    response.Message = "Plate already exists";
                    response.Status = false;
                    return response;
                }

                var vehicle = new VehicleModel
                {
                    Indentifier = vehicleCreateDto.Indentifier,
                    Year = vehicleCreateDto.Year,
                    Model = vehicleCreateDto.Model,
                    Plate = vehicleCreateDto.Plate
                };

                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                // _logger.LogInformation("Vehicle created with Id {Id}", vehicle.Id);

                // var message = System.Text.Json.JsonSerializer.Serialize(vehicle);
                // _messagingService.PublishVehicleCreated(message);

                response.Data = vehicle;
                response.Status = true;
                response.Message = "Vehicle created successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating vehicle");
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

                var hasRentals = await _context.Rentals.AnyAsync(r => r.VehicleId == idVehicle);
                if (hasRentals)
                {
                    response.Message = "Cannot delete vehicle: there are rentals associated with this vehicle.";
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
