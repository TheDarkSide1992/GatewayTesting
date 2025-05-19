using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ControllerA : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> getFromA()
    {
        return result.Any() ? Ok("ControllerA") : NotFound();
    }
}
