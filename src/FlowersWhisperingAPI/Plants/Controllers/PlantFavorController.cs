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
            var FavorPlants = _plantFavorService.GetFavorPlants(userId);
            if (FavorPlants == null )
            {
                return BadRequest("获取失败"); // 返回 400 Bad Request
            }
            if (!FavorPlants.Any())
            {
                return Ok("您还没有收藏任何植物"); // 返回 200 OK 和提示信息
            }
            return Ok(FavorPlants); // 返回 200 OK 和 latestPlants
        }
    }   
}