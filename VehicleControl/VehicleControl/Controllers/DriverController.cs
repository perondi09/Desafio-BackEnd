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

        [HttpPost("{Id}/Cnh/")]
        public async Task<IActionResult> UploadCnhImage(int Id, IFormFile cnhImage)
        {
            if (cnhImage == null || cnhImage.Length == 0)
                return BadRequest("No file uploaded");

            var allowedExtensions = new[] { ".png", ".bmp" };
            var extension = Path.GetExtension(cnhImage.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
                return BadRequest("Invalid file type. Only PNG and BMP are allowed.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "cnh");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"driver_{Id}_cnh{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cnhImage.CopyToAsync(stream);
            }
           
            var driverUpdateDto = new DriverUpdateDto { CnhImage = filePath };
            var driver = await _driverInterface.CnhUpdate(Id, driverUpdateDto);
            return Ok(driver);
        }
    }
}