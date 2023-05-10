namespace UPB.CoreLogic.Models;


public class Campaigns
{
    public Guid Id {get; set;}//Guid
    public String? Name { get; set; }
    public String? Type { get; set; }
    public String? Description { get; set; }
    public Boolean Enable {get; set;}
    public RestaurantPartner? RestaurantPartner {get; set;}
}
