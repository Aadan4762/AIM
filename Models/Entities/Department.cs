namespace AIM.Models.Entities;

public class Department
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    
    // Navigation property
    public ICollection<User> Users { get; set; }
}