using Microsoft.AspNetCore.Mvc;
using src.Service;

namespace DefaultNamespace;

[ApiController]
[Route("[controller]")]
public class JwtController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;
    
    public JwtController(JwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost]
    public IActionResult CreateToken([FromBody] CreateTokenOptions options)
    {
        Console.WriteLine("Creating JWT Token");
        var token = _jwtTokenService.CreateToken(options);
        return Ok(token);
    }
}