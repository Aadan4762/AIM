using Microsoft.AspNetCore.Identity;

namespace AIM.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string EmployeeNumber { get; set; }
}
