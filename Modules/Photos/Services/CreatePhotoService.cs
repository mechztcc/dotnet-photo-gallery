



using AppApi.Data;
using AppApi.Exceptions;
using AppApi.Modules.Photos.DTOs;
using AppApi.Modules.Photos.Models;

namespace AppApi.Modules.Photos.Services;


public class CreatePhotoService
{

    private AppDbContext _context;
    public CreatePhotoService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<object> Execute(CreatePhotoDTO payload)
    {
        var galleryExists = await _context.Galleries.FindAsync(payload.GalleryId);
        if (galleryExists == null)
        {
            throw new GlobalError("Gallery not found.", 404);
        }

        if (galleryExists.UserId != payload.UserId)
        {
            throw new GlobalError("Provided user do not have permissions to add photos in this gallery", 401);
        }

        var photo = new Photo
        {
            Base64 = payload.Base64,
            Description = payload.Description,
            Name = payload.Name,
            GalleryId = payload.GalleryId
        };

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();

      
        return new
        {
            id = photo.Id,
            name = photo.Name,
            description = photo.Description,
            base64 = photo.Base64,
            createdAt = photo.CreatedAt,
            updatedAt = photo.UpdatedAt
        };
    }
}