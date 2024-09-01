using FlowersWhisperingAPI.Administrator.Mappers;
using FlowersWhisperingAPI.Administrator.Models;
using FlowersWhisperingAPI.Administrator.Services.Interface;

namespace FlowersWhisperingAPI.Administrator.Services
{
    public class AdminUserService(AdminUserMapper userMapper) : IAdminUserService
    {        
        private readonly AdminUserMapper _userMapper = userMapper;

        public void ChangeUserState(int id,string state)
        {
            _userMapper.ChangeUserState(id,state);
        }

        public bool Feedback(int id, string feedback)
        {
            return _userMapper.Feedback(id, feedback);
        }
        public List<Feedback> GetAllFeedback()
        {
            return _userMapper.GetAllFeedback();
        }
    }
}