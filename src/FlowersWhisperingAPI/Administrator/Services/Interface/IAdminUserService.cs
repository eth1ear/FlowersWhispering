using FlowersWhisperingAPI.Administrator.Models;

namespace FlowersWhisperingAPI.Administrator.Services.Interface
{
    public interface IAdminUserService
    {
        public void ChangeUserState(int id,string state);
        public bool Feedback(int id,string feedback);
        public List<Feedback> GetAllFeedback();
        public List<UserAdmin> GetAllUsers();
    }
}