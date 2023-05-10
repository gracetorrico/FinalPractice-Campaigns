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
            //_idCounter = _campaigns.Last().Id;
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

    public Campaigns GetById(Guid id)
    {
        if (id==Guid.Empty)
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

    public Campaigns Update(Guid id, Campaigns campaign)
    {
        if (id==Guid.Empty)
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

    public Campaigns Enable(Guid id)
    {
        if (id==Guid.Empty)
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

    public Campaigns Disable(Guid id)
    {
        if (id==Guid.Empty)
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

            //campaign.Id = Interlocked.Increment(ref _idCounter);
            campaign.Id = Guid.NewGuid();
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
        _campaigns.Add(campaign);
        UpdateFile();
        return campaign;
    }

    public Campaigns Delete(Guid id)
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

    public async Task<RestaurantPartner> SearchPartners(Guid id)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync("https://random-data-api.com/api/restaurant/random_restaurant");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al buscar auspiciador");
            }

            var json = await response.Content.ReadAsStringAsync();
            var restaurantPartner = JsonConvert.DeserializeObject<RestaurantPartner>(json);

            Campaigns campaignFound = _campaigns.Find(campaign => campaign.Id == id);
            if (campaignFound ==null)
            {
                throw new Exception("Campaign not found");
            }
            if (campaignFound.Enable)
            {
                campaignFound.RestaurantPartner = new RestaurantPartner()
                {
                    Name = restaurantPartner.Name,
                    Description = restaurantPartner.Description,
                    PhoneNumber = restaurantPartner.PhoneNumber
                };
            }
            UpdateFile();
            return campaignFound.RestaurantPartner;
        }
    }

    public async Task<RestaurantPartner> SearchActivePartners()
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync("https://random-data-api.com/api/restaurant/random_restaurant");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al buscar auspiciador");
            }

            var json = await response.Content.ReadAsStringAsync();
            var restaurantPartner = JsonConvert.DeserializeObject<RestaurantPartner>(json);
            var activeCampaigns = _campaigns.Where(c => c.Enable);
            if (!activeCampaigns.Any())
            {
                throw new Exception("No hay campañas activas");
            }
            foreach(Campaigns c in activeCampaigns)
            {
                c.RestaurantPartner = new RestaurantPartner()
                {
                    Name = restaurantPartner.Name,
                    Description = restaurantPartner.Description,
                    PhoneNumber = restaurantPartner.PhoneNumber
                };
            }

            UpdateFile();
            return restaurantPartner;
        }
    }

}