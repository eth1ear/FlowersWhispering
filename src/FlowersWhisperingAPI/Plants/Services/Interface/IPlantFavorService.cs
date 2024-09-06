using FlowersWhisperingAPI.Plants.Models;

namespace FlowersWhisperingAPI.Plants.Services.Interface
{
    public interface IPlantFavorService
    {
        //
        public List<Plant> GetFavorPlants (int userId);
    }

}