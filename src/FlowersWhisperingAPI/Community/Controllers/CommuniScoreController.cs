using FlowersWhisperingAPI.Community.Models;
using FlowersWhisperingAPI.Community.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Community.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CommuniScoreController(ICommuniScoreService communiScoreService) : ControllerBase
{

    [HttpGet("score/rank")]
    public IActionResult RankScores()
    {
        return Ok(communiScoreService.RankScores());
    }
    
    [HttpGet("score/user")]
    public IActionResult GetUserScore(int userId)
    {
        return Ok(communiScoreService.GetUserScore(userId));
    }
   

}