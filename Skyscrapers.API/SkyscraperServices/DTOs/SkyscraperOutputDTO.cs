using Skyscrapers.Data.Models;
using System.Text.Json.Serialization;

namespace Skyscrapers.Services.DTOs
{
    public class SkyscraperOutputDTO
    {
        public SkyscraperOutputDTO(Skyscraper model)
        {
            this.Id = model.Id;
            this.Title = model.Title;
            this.City = model.City.Name;
            this.Country = model.City.Country.Name;
            this.Built = model.Built;
            this.OfficialHeightInMeters = model.OfficialHeightInMeters;
            this.NrOfFloors = model.NrOfFloors;
            this.Status = model.Status.ToString();
        }

        [JsonIgnore()]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("built")]
        public int Built { get; set; }

        [JsonPropertyName("official_height_in_meters")]
        public int OfficialHeightInMeters { get; set; }

        [JsonPropertyName("number_of_floors")]
        public int NrOfFloors { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

    }
}
