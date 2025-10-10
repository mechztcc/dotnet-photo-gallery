

using System.ComponentModel.DataAnnotations;

namespace AppApi.Modules.Photos.DTOs;


public class CreatePhotoDTO
{

    [Required(ErrorMessage = "Name is required")]
    public required String Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public required String Description { get; set; }

    [Required(ErrorMessage = "Base64 is required")]
    public required String Base64 { get; set; }

    [Required(ErrorMessage = "GalleryId is required")]
    public required int GalleryId { get; set; }

    public int UserId { get; set; }
}