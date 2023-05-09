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
    }

    [HttpGet]
    public List<Campaigns> Get()
    {
        return _campaignManager.GetAll();
    }

    [HttpGet]
    [Route("{id}")]
    public Campaigns GetId([FromRoute] int id)
    {
        return _campaignManager.GetById(id);
    }

    [HttpPut]
    [Route("{id}")]
    public Campaigns Put([FromRoute] int id, [FromBody] Campaigns CampaignUpdate)
    {
        return _campaignManager.Update(id, CampaignUpdate);
    }

    [HttpPut]
    [Route("{id}/enable")]
    public Campaigns Enable([FromRoute] int id)
    {
        return _campaignManager.Enable(id);
    }

    [HttpPut]
    [Route("{id}/disable")]
    public Campaigns Disable([FromRoute] int id)
    {
        return _campaignManager.Disable(id);
    }
    
    [HttpPost]
    public Campaigns Post([FromBody] Campaigns campaignToCreate)
    {
        return _campaignManager.Create(campaignToCreate.Id,campaignToCreate.Name, campaignToCreate.Type, campaignToCreate.Description);
    }

    [HttpDelete]
    [Route("{id}")]
    public Campaigns Delete([FromRoute] int id)
    {
        return _campaignManager.Delete(id);
    }
}
