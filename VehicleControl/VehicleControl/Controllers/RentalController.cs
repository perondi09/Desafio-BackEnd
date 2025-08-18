using Microsoft.AspNetCore.Mvc;
using VehicleControl.DTO.Vehicle.Rental;
using VehicleControl.Services.Rental;

namespace VehicleControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalInterface _rentalInterface;

        public RentalController(IRentalInterface rentalInterface)
        {
            _rentalInterface = rentalInterface;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRental(RentalCreateDto rentalCreateDto)
        {
            var rental = await _rentalInterface.RentalCreate(rentalCreateDto);
            return Ok(rental);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalById(int id)
        {
            var rental = await _rentalInterface.GetRentalById(id);
            return Ok(rental);
        }

        [HttpPut("{id}/Return")]
        public async Task<IActionResult> ReturnRental(int id , [FromBody] RentalReturnDto rentalReturnDto)
        {
            var rental = await _rentalInterface.ReturnRental(id , rentalReturnDto);
            return Ok(rental);
        }
    }
}