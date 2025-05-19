using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ControllerB : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> getFromB()
    {
        return result.Any() ? Ok("ControllerB") : NotFound();
    }
}