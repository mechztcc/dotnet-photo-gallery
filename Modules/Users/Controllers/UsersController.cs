using Microsoft.AspNetCore.Mvc;
using AppApi.Modules.Users.Services;
using AppApi.Modules.Users.DTOs;

namespace AppApi.Modules.Users.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{

    private readonly CreateUserService _createUserService;

    private readonly LoginService _loginService;

    public UsersController(CreateUserService createUserService, LoginService loginService)
    {
        _createUserService = createUserService;
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDTO user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        
        var newUser = await _createUserService.Execute(user);
        return Ok(newUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO payload)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _loginService.Execute(payload);
        return Ok(response);
    }

}