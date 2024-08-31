using FlowersWhisperingAPI.Administrator.Models;

namespace FlowersWhisperingAPI.Administrator.Services.Interface
{
    public interface IAdminReviewService
    {
        public List<Review> GetAllReviews();
        public void Pass(int reviewId);
        public void NotPass(int reviewId);
        public Plant? SelectPlantById(int plantId);
        public void AddPlant(Plant plant);
        public void UpdatePlant(Plant plant);
        public void DeletePlant(int plantId);
    }
}