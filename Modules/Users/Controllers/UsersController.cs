using Microsoft.AspNetCore.Mvc;
using AppApi.Modules.Users.Services;
using AppApi.Modules.Users.DTOs;

namespace AppApi.Modules.Users.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{

    private readonly CreateUserService _createUserService;

    public UsersController(CreateUserService createUserService)
    {
        this._createUserService = createUserService;
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

}