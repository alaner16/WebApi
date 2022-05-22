using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingController : ControllerBase
    {

        [HttpGet(Name = "Listing")]
        public async Task<List<Listing>> Get()
        {
            List<Listing> Listings = new List<Listing>();

            var Result = new List<Listing>();
            var ApiResult = await ServiceListing.GetListing();
            if (ApiResult.IsSuccessStatusCode)
            {
                string ApiResponse = await ApiResult.Content.ReadAsStringAsync();
                Listings = JsonConvert.DeserializeObject<List<Listing>>(ApiResponse).DistinctBy(f => new Listing()).ToList();
                Listings = ServiceListing.ComplementResult(Listings, 400);
                return ServiceListing.PrepareResponse(Listings);
            }
            else
            {
                throw new Exception("Error while processing data.");
            }
        }
    }
}