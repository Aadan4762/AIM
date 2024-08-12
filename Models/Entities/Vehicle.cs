namespace AIM.Models.Entities;

public class Vehicle
{
    public int id { get; set; }
    public string vin { get; set; }
    public string number_plate { get; set; }
    public string color { get; set; }
    public string make { get; set; }
    public string model { get; set; }
    public string year { get; set; }
    public string engine_type { get; set; }
    public string body { get; set; }
    public string fuel_type { get; set; }
    public string drive_train { get; set; }
    public string sitting_capacity { get; set; }
    public string warranty { get; set; }
    public int price { get; set; }
    public string insuarance { get; set; }
    public string status { get; set; }
    public DateTime date_recorded { get; set; }
}