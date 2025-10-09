

using System.Security.Claims;
using AppApi.Modules.Galleries.DTOs;
using AppApi.Modules.Galleries.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Modules.Galleries.Controllers;

[ApiController]
[Route("galleries")]
public class GalleryController : ControllerBase
{

    private readonly CreateGalleryService _createGalleryService;


    public GalleryController(CreateGalleryService createGalleryService)
    {
        _createGalleryService = createGalleryService;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateGalleryDTO payload)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
            return Unauthorized("UserId not found in token");

        payload.UserId = int.Parse(userIdClaim);

        var newGallery = await _createGalleryService.Execute(payload);
        return Ok(newGallery);
    }
}