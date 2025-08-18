using Microsoft.AspNetCore.Mvc;
using VehicleControl.DTO.Vehicle.Driver;
using VehicleControl.Services.Driver;

namespace VehicleControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverInterface _driverInterface;

        public DriverController(IDriverInterface driverInterface)
        {
            _driverInterface = driverInterface;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver(DriverCreateDto driverCreateDto)
        {
            var driver = await _driverInterface.DriverCreate(driverCreateDto);
            return Ok(driver);
        }

        [HttpPost("{Id}/Cnh")]
        public async Task<IActionResult> UpdateCnh(int Id, DriverUpdateDto driverUpdateDto)
        {
            var driver = await _driverInterface.CnhUpdate(Id, driverUpdateDto);
            return Ok(driver);
        }
    }
}