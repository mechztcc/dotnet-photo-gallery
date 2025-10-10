


using AppApi.Data;
using AppApi.Exceptions;
using AppApi.Modules.Galleries.DTOs;

namespace AppApi.Modules.Galleries.Services;


public class UpdateGalleryService
{

    private readonly AppDbContext _context;

    public UpdateGalleryService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<object> Execute(UpdateGalleryDTO payload)
    {
        var gallery = await _context.Galleries.FindAsync(payload.GalleryId);
        if (gallery == null)
        {
            throw new GlobalError("Gallery not found.", 404);
        }

        if (gallery.UserId != payload.UserId)
        {
            throw new GlobalError("Provided user do not have permissions to update this gallery.", 401);
        }


        // _context.Entry(gallery).CurrentValues.SetValues(payload);
        
        gallery.Description = payload.Description;
        gallery.Name = payload.Name;
        gallery.IsActive = payload.IsActive;
        gallery.IsPublic = payload.IsPublic;

        await _context.SaveChangesAsync();


        return new
        {
            gallery
        };
    }

}