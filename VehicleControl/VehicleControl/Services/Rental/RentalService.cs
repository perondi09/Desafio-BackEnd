using Microsoft.EntityFrameworkCore;
using VehicleControl.Data;
using VehicleControl.DTO.Vehicle.Rental;
using VehicleControl.Models;

namespace VehicleControl.Services.Rental
{
    public class RentalService : IRentalInterface
    {

        private readonly AppDbContext _context;

        public RentalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<RentalModel>> RentalCreate(RentalCreateDto rentalCreateDto)
        {
            ResponseModel<RentalModel> response = new ResponseModel<RentalModel>();

            try
            {
                var vehicle = await _context.Vehicles.FindAsync(rentalCreateDto.VehicleId);
                if (vehicle == null)
                {
                    response.Message = "Vehicle not found";
                    response.Status = false;
                    return response;
                }

                var driver = await _context.Drivers.FindAsync(rentalCreateDto.DriverId);
                if (driver == null)
                {
                    response.Message = "Driver not found";
                    response.Status = false;
                    return response;
                }

                var rental = new RentalModel
                {
                    DriverId = rentalCreateDto.DriverId,
                    VehicleId = rentalCreateDto.VehicleId,
                    RentalDate = rentalCreateDto.RentalDate,
                    ReturnDate = rentalCreateDto.ReturnDate,
                    ExpectedReturnDate = rentalCreateDto.ExpectedReturnDate,
                    Plan = rentalCreateDto.Plan
                };

                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();

                response.Data = rental;
                response.Status = true;
                response.Message = "Rental created successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<RentalModel>> GetRentalById(int id)
        {
            ResponseModel<RentalModel> response = new ResponseModel<RentalModel>();

            try
            {
                var rental = await _context.Rentals
                    .Include(r => r.Vehicle)
                    .Include(r => r.Driver)
                    .FirstOrDefaultAsync(r => r.Id == id);
                if (rental == null)
                {
                    response.Message = "Rental not found";
                    response.Status = false;
                    return response;
                }
                response.Data = rental;
                response.Status = true;
                response.Message = "Rental retrieved successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<RentalModel>> ReturnRental(int id, RentalReturnDto rentalReturnDto)
        {
            ResponseModel<RentalModel> response = new ResponseModel<RentalModel>();

            try
            {
                var rental = await _context.Rentals.FindAsync(id);
                if (rental == null)
                {
                    response.Message = "Rental not found";
                    response.Status = false;
                    return response;
                }

                rental.ExpectedReturnDate = rentalReturnDto.ExpectedReturnDate;
                _context.Rentals.Update(rental);
                await _context.SaveChangesAsync();
                response.Data = rental;
                response.Status = true;
                response.Message = "Rental return updated successfully";
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