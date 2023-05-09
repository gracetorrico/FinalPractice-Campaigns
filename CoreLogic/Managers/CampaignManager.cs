using UPB.CoreLogic.Models;
namespace UPB.CoreLogic.Managers;

public class CampaignManager
{
    private List<Campaigns> _campaigns;
    
    public CampaignManager()
    {
        _campaigns = new List<Campaigns>();
    }

    public List<Campaigns> Init()
    {
        return _campaigns;
    }
    public List<Campaigns> GetAll()
    {
        return _campaigns;
    }

    public Campaigns GetById()
    {
        return new Campaigns();
    }

    public Campaigns Update()
    {
        return new Campaigns();
    }

    public Campaigns Create(string name, string type, string description)
    {
        Campaigns campaign; 

        try
        {
            campaign = new Campaigns()
            {
                Name = name,
                Type = type,
                Description = description,
                Enable = false,
                RestaurantPartner = null
            };
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
        _campaigns.Add(campaign);
        return campaign;
    }

    public Campaigns Delete(string name, string type)
    {
        int campaignsToDeleteIndex = _campaigns.FindIndex(campaigns => campaigns.Name == name && campaigns.Type == type);
        Campaigns campaignsToDelete = _campaigns[campaignsToDeleteIndex];
        _campaigns.RemoveAt(campaignsToDeleteIndex);
        return campaignsToDelete;
    }

}