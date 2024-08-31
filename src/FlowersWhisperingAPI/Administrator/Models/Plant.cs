namespace FlowersWhisperingAPI.Administrator.Models
{
    public class Plant(int plantId, string commonName, string scientificName, string category, string portrayal, string growthEnvironment, string careConditions)
    {
        public int PlantId { get; set; } = plantId;
        public string CommonName { get; set; } = commonName;
        public string ScientificName { get; set; } = scientificName;
        public string Category { get; set; } = category;
        public string Portrayal { get; set; } = portrayal;
        public string GrowthEnvironment { get; set; } = growthEnvironment;
        public string CareConditions { get; set; } = careConditions;
    }
}