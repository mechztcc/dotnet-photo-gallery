

using System.ComponentModel.DataAnnotations;

namespace AppApi.Modules.Galleries.DTOs;

public class CreateGalleryDTO
{

    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "IsPublic is required")]
    public required bool IsPublic { get; set; }

    public int UserId { get; set; }
}