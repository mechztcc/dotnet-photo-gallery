

using AppApi.Data;
using AppApi.Exceptions;
using AppApi.Modules.Galleries.DTOs;
using AppApi.Modules.Galleries.Models;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Modules.Galleries.Services;

public class CreateGalleryService
{
    private readonly AppDbContext _context;


    public CreateGalleryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Gallery> Execute(CreateGalleryDTO payload)
    {
       
        var gallery = new Gallery
        {
            Name = payload.Name,
            Description = payload.Description,
            IsActive = true,
            IsPublic = payload.IsPublic,
            UserId = payload.UserId
        };

        _context.Galleries.Add(gallery);
        await _context.SaveChangesAsync();

        return gallery;
    }

}