namespace AIM.Dtos.EntityDtos;

public class FurnitureDto
{
    public string type { get; set; }
    public string tag { get; set; }
    public string material { get; set; }
    public string color { get; set; }
    public string dimension { get; set; }
    public int cost { get; set; }
    public string status { get; set; }
    public string upload_image { get; set; }
    public DateTime date_recorded { get; set; }
}