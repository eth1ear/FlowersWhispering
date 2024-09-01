using FlowersWhisperingAPI.Administrator.Mappers;
using FlowersWhisperingAPI.Administrator.Models;
using FlowersWhisperingAPI.Administrator.Services.Interface;

namespace FlowersWhisperingAPI.Administrator.Services
{
    public class AdminReviewService(AdminReviewMapper reviewMapper) : IAdminReviewService
    {        
        private readonly AdminReviewMapper _reviewMapper = reviewMapper;
        public List<Review> GetAllReviews()
        {
            List<Review> reviews = _reviewMapper.GetAllReviews();
            List<Review> sortedReviews = reviews.OrderBy(review => review.ReviewId).ToList();
            return sortedReviews;
        }

        public void Pass(int reviewId)
        {
            _reviewMapper.UpdateReviewStatus(reviewId,"审核通过");
        }

        public void NotPass(int reviewId)
        {
            _reviewMapper.UpdateReviewStatus(reviewId,"不通过");
        }

        public Plant? SelectPlantById(int plantId)
        {
            return _reviewMapper.SelectPlantById(plantId);
        }

        public void AddPlant(Plant plant)
        {
            _reviewMapper.AddPlant(plant.CommonName, plant.ScientificName, plant.Category, plant.Portrayal, plant.GrowthEnvironment, plant.CareConditions);
        }

        public void UpdatePlant(Plant plant)
        {
            _reviewMapper.UpdatePlant(plant.PlantId, plant.CommonName, plant.ScientificName, plant.Category, plant.Portrayal, plant.GrowthEnvironment, plant.CareConditions);
        }

        public void DeletePlant(int plantId)
        {
            _reviewMapper.DeletePlant(plantId);
        }
    }
}