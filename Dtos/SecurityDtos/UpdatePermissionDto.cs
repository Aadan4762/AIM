using System.ComponentModel.DataAnnotations;

namespace AIM.Dtos;

public class UpdatePermissionDto
{
    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; } 
}