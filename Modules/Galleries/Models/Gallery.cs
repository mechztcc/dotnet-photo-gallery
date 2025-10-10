
using AppApi.Modules.Photos.Models;
using AppApi.Modules.Shared.Models;
using AppApi.Modules.Users.Models;

namespace AppApi.Modules.Galleries.Models;

public class Gallery : BaseEntity
{

    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public bool IsActive { get; set; }

    public bool IsPublic { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
}


