using FlowersWhisperingAPI.Plants.Services.Interface;
using FlowersWhisperingAPI.Plants.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Plants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantFavorController(IPlantFavorService plantFavorService) : ControllerBase
    {
        private readonly IPlantFavorService _plantFavorService = plantFavorService;

        [HttpGet("findFavors")]
        public IActionResult GetFavorPlants (int userId)
        {
            List<Plant> FavorPlants = _plantFavorService.GetFavorPlants(userId);
            if (FavorPlants == null )
            {
                return NotFound(); // 返回 404 Not Found
            }
            if (!FavorPlants.Any())
            {
                var response = new
                {
                    Message = "No favorite plants found for the given user.",
                };
                return Ok(response);
            }
            return Ok(FavorPlants); // 返回 200 OK 和 latestPlants
        }
    }   
}