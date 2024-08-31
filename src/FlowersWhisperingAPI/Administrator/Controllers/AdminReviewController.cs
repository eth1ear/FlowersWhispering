using FlowersWhisperingAPI.Administrator.Models;
using FlowersWhisperingAPI.Administrator.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminReviewController(IAdminReviewService adminReviewService) : ControllerBase
    {
        private readonly IAdminReviewService _adminReviewService = adminReviewService;

        [HttpPost("all")]
        public IActionResult GetAllReviews()
        {
            return Ok(_adminReviewService.GetAllReviews());
        }

        [HttpPut("pass")]
        public void Pass(int reviewId)
        {
            _adminReviewService.Pass(reviewId);
        }

        [HttpPut("pass/no")]
        public void NotPass(int reviewId)
        {
            _adminReviewService.NotPass(reviewId);
        }

        [HttpGet("plant/select")]
        public IActionResult SelectPlantById(int plantId)
        {
            var plant = _adminReviewService.SelectPlantById(plantId);
            if(plant != null)
                return Ok(plant);
            else
                return BadRequest("该植物不存在");
        }

        [HttpPut("plant/update")]
        public void UpdatePlant([FromBody] Plant plant)
        {
            _adminReviewService.UpdatePlant(plant);
        }

        [HttpPost("plant/add")]
        public void AddPlant([FromBody] Plant plant)
        {
            _adminReviewService.AddPlant(plant);
        }

        [HttpDelete("plant/delete")]
        public void DeletePlant(int plantId)
        {
            _adminReviewService.DeletePlant(plantId);
        }
    }
}