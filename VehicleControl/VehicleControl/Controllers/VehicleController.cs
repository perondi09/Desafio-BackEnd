using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleControl.DTO.Vehicle;
using VehicleControl.Models;
using VehicleControl.Services.Vehicle;

namespace VehicleControl.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleInterface _vehicleInterface;

        public VehicleController(IVehicleInterface vehicleInterface)
        {
            _vehicleInterface = vehicleInterface;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(VehicleCreateDto vehicleCreateDto)
        {
            var vehicle = await _vehicleInterface.CreateVehicle(vehicleCreateDto);
            return Ok(vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleByPlate(string Plate)
        {
            var vehicles = await _vehicleInterface.GetVehicleByPlate(Plate);
            return Ok(vehicles);
        }

        [HttpPut("{Id}/Plate")]
        public async Task<IActionResult> UpdateVehicle(int Id, VehicleUpdateDto vehicleUpdateDto)
        {
            var vehicle = await _vehicleInterface.UpdateVehicle(Id, vehicleUpdateDto);
            return Ok(vehicle);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetVehicleById(int Id)
        {
            var vehicles = await _vehicleInterface.GetVehicleById(Id);
            return Ok(vehicles);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteVehicle(int Id)
        {
            var vehicle = await _vehicleInterface.DeleteVehicle(Id);
            return Ok(vehicle);
        }
    }
}
