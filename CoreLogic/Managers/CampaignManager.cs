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

    public Campaigns GetByType(string type)
    {
        Campaigns campaignFound = _campaigns.Find(campaing => campaing.Type == type);
        if(campaignFound==null)
        {
            throw new Exception("“Campaign not found");
        }
        return campaignFound;
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
                Enable = false
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