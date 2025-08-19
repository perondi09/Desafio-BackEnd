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
               
                if (driver.CnhCategory != CnhCategoryType.A && driver.CnhCategory != CnhCategoryType.AB)
                {
                    response.Message = "Only drivers with CNH category A or AB can rent a motorcycle.";
                    response.Status = false;
                    return response;
                }

                var planDays = 0;
                var dailyRate = 0m;
                var plan = rentalCreateDto.Plan.ToLower();
                switch (plan)
                {
                    case "7": planDays = 7; dailyRate = 30m; break;
                    case "15": planDays = 15; dailyRate = 28m; break;
                    case "30": planDays = 30; dailyRate = 22m; break;
                    case "45": planDays = 45; dailyRate = 20m; break;
                    case "50": planDays = 50; dailyRate = 18m; break;
                    default:
                        response.Message = "Invalid rental plan.";
                        response.Status = false;
                        return response;
                }
              
                var totalValue = planDays * dailyRate;

                var rental = new RentalModel
                {
                    DriverId = rentalCreateDto.DriverId,
                    VehicleId = rentalCreateDto.VehicleId,
                    RentalDate = rentalCreateDto.RentalDate,
                    ReturnDate = rentalCreateDto.ReturnDate,
                    ExpectedReturnDate = rentalCreateDto.ExpectedReturnDate,
                    Plan = rentalCreateDto.Plan,
                    Price = totalValue
                };

                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();

                response.Data = rental;
                response.Status = true;
                response.Message = $"Rental created successfully. Price: {totalValue:C}";
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

                // Datas
                var actualReturnDate = rentalReturnDto.ExpectedReturnDate;
                var expectedReturnDate = rental.ExpectedReturnDate;
                var rentalStartDate = rental.RentalDate;

                // Planos e valores
                var planDays = 0;
                var dailyRate = 0m;
                var plan = rental.Plan.ToLower();
                switch (plan)
                {
                    case "7": planDays = 7; dailyRate = 30m; break;
                    case "15": planDays = 15; dailyRate = 28m; break;
                    case "30": planDays = 30; dailyRate = 22m; break;
                    case "45": planDays = 45; dailyRate = 20m; break;
                    case "50": planDays = 50; dailyRate = 18m; break;
                    default:
                        response.Message = "Invalid rental plan.";
                        response.Status = false;
                        return response;
                }

                // Cálculo de diárias efetivadas
                var totalDays = (actualReturnDate - rentalStartDate).Days;
                if (totalDays < 1) totalDays = 1;

                decimal totalValue = 0m;
                decimal multa = 0m;
                decimal extra = 0m;

                if (actualReturnDate < expectedReturnDate)
                {
                    // Devolução antecipada
                    var unusedDays = (expectedReturnDate - actualReturnDate).Days;
                    var usedDays = totalDays;
                    totalValue = usedDays * dailyRate;
                    switch (planDays)
                    {
                        case 7: multa = unusedDays * dailyRate * 0.2m; break;
                        case 15: multa = unusedDays * dailyRate * 0.4m; break;
                        default: multa = 0m; break;
                    }
                }
                else if (actualReturnDate > expectedReturnDate)
                {
                    // Devolução tardia
                    var extraDays = (actualReturnDate - expectedReturnDate).Days;
                    totalValue = planDays * dailyRate + (extraDays * 50m);
                    extra = extraDays * 50m;
                }
                else
                {
                    // Devolução no prazo
                    totalValue = planDays * dailyRate;
                }

                rental.ReturnDate = actualReturnDate;
                rental.Price = totalValue + multa + extra;
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