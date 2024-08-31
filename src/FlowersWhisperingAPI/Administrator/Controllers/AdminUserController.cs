using FlowersWhisperingAPI.Administrator.Models;
using FlowersWhisperingAPI.Administrator.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController(IAdminUserService adminUserService) : ControllerBase
    {
        private readonly IAdminUserService _adminUserService = adminUserService;

        [HttpPut("ban/user")]
        public void BanUser(int id)
        {
           _adminUserService.ChangeUserState(id,"banned");
        }

        [HttpPut("unblock/user")]
        public void NoBanUser(int id)
        {
           _adminUserService.ChangeUserState(id,"active");
        }

        [HttpPost("user/feedback")]
        public void Feedback([FromBody] FeedbackDTO feedbackDTO)
        {
            if(feedbackDTO.FeedbackContent != null)
                _adminUserService.Feedback(feedbackDTO.UserId,feedbackDTO.FeedbackContent);
        }

        [HttpGet("all/feedback")]
        public IActionResult GetAllFeedback()
        {
            return Ok(_adminUserService.GetAllFeedback());
        }
    }
}