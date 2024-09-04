using FlowersWhisperingAPI.Plants.Services.Interface;
using FlowersWhisperingAPI.Plants.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Plants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantFindController(IPlantFindService plantFindService) : ControllerBase
    {
        private readonly IPlantFindService _plantFindService = plantFindService;

        [HttpGet("findId")]
        public IActionResult GetPlantId (string plantName)
        {
            var plantId = _plantFindService.GetPlantId(plantName);
            if (plantId == -1)
            {
                return NotFound(); // 返回 404 Not Found
            }
            return Ok(plantId); // 返回 200 OK 和 plantId
        }
    }   
}