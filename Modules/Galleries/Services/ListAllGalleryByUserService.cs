

using AppApi.Data;
using AppApi.Modules.Galleries.DTOs;
using AppApi.Modules.Galleries.Models;
using AppApi.Modules.Photos.Models;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Modules.Galleries.Services;


public class ListAllGalleryByUserService
{
    private readonly AppDbContext _context;

    public ListAllGalleryByUserService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<object> Execute(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.Galleries.AsQueryable();

        var totalCount = await query.CountAsync();
        var galleries = await query
        .Where(g => g.UserId == userId)
        .OrderBy(g => g.Id)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .Select(g => new Gallery
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            Photos = g.Photos.Select(p => new Photo
            {
                Id = p.Id,
                Name = p.Name,
                Base64 = p.Base64,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList()
        })
        .ToListAsync();

        return new
        {
            items = galleries,
            total = totalCount,
            page = pageNumber,
            pageSize,
            totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

}