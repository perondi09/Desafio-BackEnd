using VehicleControl.Data;
using VehicleControl.DTO.Vehicle.Driver;
using VehicleControl.Models;

namespace VehicleControl.Services.Driver
{
    public class DriverService : IDriverInterface
    {
        private readonly AppDbContext _context;

        public DriverService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<DriverModel>> DriverCreate(DriverCreateDto driverCreateDto)
        {
            ResponseModel<DriverModel> response = new ResponseModel<DriverModel>();

            try
            {
                var driver = new DriverModel
                {
                    Name = driverCreateDto.Name,
                    Cnpj = driverCreateDto.Cnpj,
                    BirthDate = driverCreateDto.BirthDate,
                    Cnh = driverCreateDto.Cnh,
                    CnhCategory = driverCreateDto.CnhCategory,
                    CnhImage = driverCreateDto.CnhImage
                };

                _context.Drivers.Add(driver);
                await _context.SaveChangesAsync();

                response.Data = driver;
                response.Status = true;
                response.Message = "Driver created successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<DriverModel>> CnhUpdate(int id, DriverUpdateDto driverUpdateDto)
        {
            ResponseModel<DriverModel> response = new ResponseModel<DriverModel>();

            try
            {
                var driver = await _context.Drivers.FindAsync(id);
                if (driver == null)
                {
                    response.Message = "Driver not found";
                    response.Status = false;
                    return response;
                }

                driver.CnhImage = driverUpdateDto.CnhImage;
                _context.Drivers.Update(driver);
                await _context.SaveChangesAsync();

                response.Data = driver;
                response.Status = true;
                response.Message = "Driver CNH updated successfully";

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