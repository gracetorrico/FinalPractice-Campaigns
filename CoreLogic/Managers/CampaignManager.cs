using UPB.CoreLogic.Models;
using Newtonsoft.Json;



namespace UPB.CoreLogic.Managers;

public class CampaignManager
{
    private List<Campaigns> _campaigns;
    private string _json;
    private static int _idCounter = 0;
    
    public CampaignManager()
    {
        _campaigns = new List<Campaigns>();
        _json = "..\\campaigns.json";
    }

    public List<Campaigns> Init()
    {
        return _campaigns;
    }
    public List<Campaigns> GetAll()
    {
        return _campaigns;
    }

    public Campaigns GetById(int id)
    {
        if (id<0)
        {
            throw new Exception("Id inválido");
        }
        Campaigns campaignFound = _campaigns.Find(campaing => campaing.Id == id);
        if(campaignFound==null)
        {
            throw new Exception("“Campaign not found");
        }
        return campaignFound;
    }

    public Campaigns Update(int id, Campaigns campaign)
    {
        if (id<0)
        {
            throw new Exception("CI inválido");
        }
        Campaigns campaignFound = _campaigns.Find(campaign => campaign.Id == id);
        if (campaignFound ==null)
        {
            throw new Exception("Patient not found");
        }
        campaignFound.Name = campaign.Name;
        campaignFound.Type = campaign.Type;
        campaignFound.Description = campaign.Description;
        //UpdateFile();
        return campaignFound;
    }

    public Campaigns Enable(int id)
    {
        if (id<0)
        {
            throw new Exception("CI inválido");
        }
        Campaigns campaignFound = _campaigns.Find(campaign => campaign.Id == id);
        if (campaignFound ==null)
        {
            throw new Exception("Patient not found");
        }
        campaignFound.Enable = true;
        return campaignFound;
    }

    public Campaigns Disable(int id)
    {
        if (id<0)
        {
            throw new Exception("CI inválido");
        }
        Campaigns campaignFound = _campaigns.Find(campaign => campaign.Id == id);
        if (campaignFound ==null)
        {
            throw new Exception("Patient not found");
        }
        campaignFound.Enable = false;
        return campaignFound;
    }

    public Campaigns Create(int id, string name, string type, string description)
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

            campaign.Id = Interlocked.Increment(ref _idCounter);
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
        _campaigns.Add(campaign);


        //reading all the json file to add a new line
        string json = File.ReadAllText(_json);

        // serializing created object and adding to the json file
        json += "\n"+ JsonConvert.SerializeObject(campaign);
        File.WriteAllText(_json, json);

        return campaign;
    }

    public Campaigns Delete(int id)
    {
        int campaignsToDeleteIndex = _campaigns.FindIndex(campaigns => campaigns.Id == id);
        if (campaignsToDeleteIndex==-1)
        {
            throw new Exception("CI inválido");
        }
        Campaigns campaignsToDelete = _campaigns[campaignsToDeleteIndex];
        _campaigns.RemoveAt(campaignsToDeleteIndex);
        //UpdateFile();
        return campaignsToDelete;
    }

}