

using AppApi.Modules.Galleries.Models;
using AppApi.Modules.Shared.Models;

namespace AppApi.Modules.Photos.Models;

public class Photo : BaseEntity
{
    public int Id { get; set; }

    public String Name { get; set; } = "";

    public String Description { get; set; } = "";

    public String Base64 { get; set; } = "";

    public int GalleryId { get; set; }

    public Gallery Gallery { get; set; } = null!;
}