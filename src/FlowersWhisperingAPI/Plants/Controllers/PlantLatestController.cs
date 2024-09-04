using FlowersWhisperingAPI.Plants.Services.Interface;
using FlowersWhisperingAPI.Plants.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Plants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantLatestController(IPlantLatestService plantLatestService) : ControllerBase
    {
        private readonly IPlantLatestService _plantLatestService = plantLatestService;

        [HttpGet("findLatest")]
        public IActionResult GetLatestPlants ()
        {
            List<Plant> latestPlants = _plantLatestService.GetLatestPlants();
            if (latestPlants == null || !latestPlants.Any())
            {
                return NotFound(); // 返回 404 Not Found
            }
            return Ok(latestPlants); // 返回 200 OK 和 latestPlants
        }
        
    }   
}