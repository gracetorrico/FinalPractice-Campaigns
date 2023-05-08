using Microsoft.AspNetCore.Mvc;
using UPB.CoreLogic.Managers;
using UPB.CoreLogic.Models;

namespace FinalPracticeCampaigns.Controllers;

[ApiController]
[Route("campaigns")]
public class CampaignsController : ControllerBase
{
    private readonly CampaignManager _campaignManager;
    public CampaignsController(CampaignManager campaignManager)
    {
        _campaignManager= campaignManager; 
        _campaignManager.Init();
    }

    [HttpGet]
    public List<Campaigns> Get()
    {
        return _campaignManager.GetAll();
    }

    [HttpGet]
    [Route("{id}")]
    public Campaigns GetById([FromRoute] int id)
    {
        return _campaignManager.GetById();
    }

    [HttpPut]
    [Route("{id}")]
    public Campaigns Put([FromRoute] int id, [FromBody] Campaigns patientToUpdate)
    {
        return _campaignManager.Update();
    }

    [HttpPost]
    public Campaigns Post([FromBody] Campaigns patientToCreate)
    {
        return _campaignManager.Create();
    }

    [HttpDelete]
    [Route("{id}")]
    public Campaigns Delete([FromRoute] string name, string type)
    {
        return _campaignManager.Delete();
    }
}
