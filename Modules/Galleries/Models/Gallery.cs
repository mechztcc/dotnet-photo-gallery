
using AppApi.Modules.Shared.Models;
using AppApi.Modules.Users.Models;

namespace AppApi.Modules.Galleries.Models;

public class Gallery : BaseEntity
{

    public int Id { get; set; }

    public string Name { get; set; } = "";

    public bool isActive { get; set; }

    public bool isPublic { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}


