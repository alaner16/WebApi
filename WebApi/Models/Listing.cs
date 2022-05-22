using System.Text.Json;
using System.Text.Json.Serialization;
namespace WebApi
{
    public class Listing
    {
        public long ListingId { get; set; }

        public string Company { get; set; }

        public Uri Image_List { get; set; }

        [JsonIgnore]
        public long CategoryId { get; set; }

    }
}