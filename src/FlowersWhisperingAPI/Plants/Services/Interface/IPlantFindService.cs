using FlowersWhisperingAPI.Plants.Models;

namespace FlowersWhisperingAPI.Plants.Services.Interface
{
    public interface IPlantFindService
    {
        public int GetPlantId (string plantName);
    }

}