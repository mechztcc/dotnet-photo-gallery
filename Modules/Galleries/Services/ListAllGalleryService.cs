

using AppApi.Data;
using AppApi.Modules.Galleries.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Modules.Galleries.Services;


public class ListAllGalleryService
{
    private readonly AppDbContext _context;

    public ListAllGalleryService(AppDbContext context)
    {
        _context = context;
    }



    public async Task<object> Execute(int pageNumber = 1, int pageSize = 10)
    {

        var query = _context.Galleries.AsQueryable();

        var totalCount = await query.CountAsync();

        var galleries = await query
            .OrderBy(g => g.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
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