using AppApi.Modules.Galleries.Models;
using AppApi.Modules.Shared.Models;

namespace AppApi.Modules.Users.Models;

public class User : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public ICollection<Gallery> Galleries { get; set; } = new List<Gallery>();
}