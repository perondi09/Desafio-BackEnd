using Microsoft.AspNetCore.Mvc;
using VehicleControl.Data;

namespace VehicleControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HealthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CheckDatabase()
        {
            try
            {
                // Tenta acessar o banco de dados
                var canConnect = _context.Database.CanConnect();
                if (canConnect)
                    return Ok("Database connection OK");
                else
                    return StatusCode(500, "Database connection FAILED");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database error: {ex.Message}");
            }
        }
    }
}
