using UPB.FinalPracticeCampaigns.Models;
namespace UPB.FinalPracticeCampaigns.Managers;

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

    public Campaigns Create()
    {
        return new Campaigns();
    }

    public Campaigns Delete()
    {
        return new Campaigns();
    }

}