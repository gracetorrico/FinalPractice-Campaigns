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
    public Campaigns GetId([FromRoute] Guid id)
    {
        return _campaignManager.GetById(id);
    }

    [HttpPut]
    [Route("{id}")]
    public Campaigns Put([FromRoute] Guid id, [FromBody] Campaigns CampaignUpdate)
    {
        return _campaignManager.Update(id, CampaignUpdate);
    }

    [HttpPut]
    [Route("{id}/enable")]
    public Campaigns Enable([FromRoute] Guid id)
    {
        return _campaignManager.Enable(id);
    }

    [HttpPut]
    [Route("{id}/disable")]
    public Campaigns Disable([FromRoute] Guid id)
    {
        return _campaignManager.Disable(id);
    }

    [HttpPut]
    [Route("{id}/search-partners")]
    public void SearchPartners([FromRoute] Guid id)
    {
        _campaignManager.SearchPartners(id);
    }

    [HttpPut]
    [Route("active/search-partners")]
    public void SearchActivePartners()
    {
        _campaignManager.SearchActivePartners();
    }
    
    [HttpPost]
    public Campaigns Post([FromBody] Campaigns campaignToCreate)
    {
        return _campaignManager.Create(campaignToCreate.Name, campaignToCreate.Type, campaignToCreate.Description);
    }

    [HttpDelete]
    [Route("{id}")]
    public Campaigns Delete([FromRoute] Guid id)
    {
        return _campaignManager.Delete(id);
    }
}
