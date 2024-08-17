namespace AIM.Models.Entities;

public class User
{
    public int id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string employee_no { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string role { get; set; }
    public string password { get; set; }
    public string user_image { get; set; }
    // New properties for Department
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}