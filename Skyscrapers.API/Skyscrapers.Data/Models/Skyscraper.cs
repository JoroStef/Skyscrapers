using Skyscrapers.Data.Models.Enums;

namespace Skyscrapers.Data.Models
{
    public class Skyscraper
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int Built { get; set; }
        public int OfficialHeightInMeters { get; set; }
        public int NrOfFloors { get; set; }
        public StatusType Status { get; set; }
    }
}
