


using System.Security.Claims;
using AppApi.Modules.Photos.DTOs;
using AppApi.Modules.Photos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Modules.Photos.Controllers;

[ApiController]
[Route("photos")]
public class PhotosController : ControllerBase
{

    private CreatePhotoService _createPhotoService;

    public PhotosController(CreatePhotoService createPhotoService)
    {
        _createPhotoService = createPhotoService;
    }


    [Authorize]
    [HttpPost("upload")]
    public async Task<IActionResult> Create([FromBody] CreatePhotoDTO payload)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
            return Unauthorized("UserId not found in token");


        payload.UserId = int.Parse(userIdClaim);
        var photo = await _createPhotoService.Execute(payload);
        return Ok(photo);
    }

}