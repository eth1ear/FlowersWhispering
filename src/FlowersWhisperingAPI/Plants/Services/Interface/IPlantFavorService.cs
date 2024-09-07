using FlowersWhisperingAPI.Plants.Models;

namespace FlowersWhisperingAPI.Plants.Services.Interface
{
    public interface IPlantFavorService
    {
        //
        public List<Plant> GetFavorPlants (int userId);
        public bool AddFavorPlant (int userId, string plantName);
        public bool DeleteFavorPlant (int userId, string plantName);
    }

}