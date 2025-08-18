using VehicleControl.DTO.Vehicle.Rental;
using VehicleControl.Models;

namespace VehicleControl.Services.Rental
{
    public interface IRentalInterface
    {
        Task<ResponseModel<RentalModel>> RentalCreate(RentalCreateDto rentalCreateDto);
        Task<ResponseModel<RentalModel>> GetRentalById(int id);
        Task<ResponseModel<RentalModel>> ReturnRental(int id, RentalReturnDto rentalReturnDto);
    }
}