

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

    private readonly ListAllGalleryService _listAllGalleryService;


    private readonly ListAllGalleryByUserService _listAllGalleryByUserService;

    public GalleryController(CreateGalleryService createGalleryService, ListAllGalleryService listAllGalleryService, ListAllGalleryByUserService listAllGalleryByUserService)
    {
        _createGalleryService = createGalleryService;
        _listAllGalleryService = listAllGalleryService;
        _listAllGalleryByUserService = listAllGalleryByUserService;
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

    [Authorize]
    [HttpGet("list")]
    public async Task<IActionResult> ListAll([FromQuery] int page, [FromQuery] int size = 10)
    {

        if (page < 1 || size < 1)
        {
            return BadRequest("Parâmetros de paginação inválidos.");
        }

        var galleries = await _listAllGalleryService.Execute(page, size);
        return Ok(galleries);
    }


    [Authorize]
    [HttpGet("list-by-user")]
    public async Task<IActionResult> ListAllByUserId([FromQuery] int page, [FromQuery] int size = 10)
    {
        if (page < 1 || size < 1)
        {
            return BadRequest("Parâmetros de paginação inválidos.");
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
            return Unauthorized("UserId not found in token");

        var userId = int.Parse(userIdClaim);

        var galleries = await _listAllGalleryByUserService.Execute(userId, page, size);
        return Ok(galleries);
    }
}