namespace TravelDestinationFrontend.Models
{
    public class DestinationViewModel
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Rating { get; set; }
        public DateTime LastVisited { get; set; }
    }
}
