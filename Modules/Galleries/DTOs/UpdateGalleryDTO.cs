


using System.ComponentModel.DataAnnotations;

namespace AppApi.Modules.Galleries.DTOs;

public class UpdateGalleryDTO
{
    [Required(ErrorMessage = "Description is required")]

    public required String Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public required String Description { get; set; }

    [Required(ErrorMessage = "IsPublic is required")]
    public required bool IsPublic { get; set; }

    [Required(ErrorMessage = "IsActive is required")]
    public required bool IsActive { get; set; }

    public int GalleryId { get; set; }

    public int UserId { get; set; }
}