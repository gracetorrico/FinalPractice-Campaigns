using UPB.CoreLogic.Models;
using Newtonsoft.Json;



namespace UPB.CoreLogic.Managers;

public class CampaignManager
{
    private List<Campaigns> _campaigns;
    private readonly string _jsonFilePath;
    private static int _idCounter;
    
    public CampaignManager()
    {
        _campaigns = new List<Campaigns>();
        _jsonFilePath = "..\\campaigns.json";
        Init();

    }

    public List<Campaigns> Init()
    {
        if (!File.Exists(_jsonFilePath))
        {
            _idCounter = 0;
            return new List<Campaigns>();
        }

        var json = File.ReadAllText(_jsonFilePath);
        if (string.IsNullOrEmpty(json))
        {
            _idCounter = 0;
            return new List<Campaigns>();
        }

        try
        {
            _campaigns = JsonConvert.DeserializeObject<List<Campaigns>>(json);
            _idCounter = _campaigns.Last().Id;
        }
        catch (JsonException ex)
        {
            throw new Exception("Error al cargar los pacientes desde el archivo JSON.", ex);
        }
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
        UpdateFile();
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
        UpdateFile();
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
        UpdateFile();
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
        UpdateFile();
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
        UpdateFile();
        return campaignsToDelete;
    }
    public void UpdateFile()
    {
        var json = JsonConvert.SerializeObject(_campaigns);
        File.WriteAllText(_jsonFilePath, json);
    }

}