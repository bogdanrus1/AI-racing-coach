using Microsoft.AspNetCore.Mvc;

namespace RacingCoach.API.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHealth()
    {
        return Ok(new
        {
            status = "Healthy",
            application = "AI Racing Coach"
        });
    }
}
