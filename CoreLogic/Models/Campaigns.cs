namespace UPB.CoreLogic.Models;

public class Campaigns
{
    public int Id {get; set;}
    public String? Name { get; set; }
    public String? Type { get; set; }
    public String? Description { get; set; }
    public Boolean Enable {get; set;}
    public object? RestaurantPartner {get; set;}
}
