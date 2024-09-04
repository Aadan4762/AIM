namespace AIM.Dtos.EntityDtos;

public class UserDto
{
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string employee_no { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string role { get; set; }
    public string password { get; set; }
    public int DepartmentId { get; set; } // Add DepartmentId property
 
}