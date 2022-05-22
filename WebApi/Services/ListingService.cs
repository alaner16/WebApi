namespace WebApi.Services
{
    public static class ServiceListing
    {
        public static async Task<HttpResponseMessage> GetListing()
        {
            HttpClient Client = new HttpClient();
            return await Client.GetAsync("https://webservice.trueomni.com/json.aspx?domainid=2248&fn=listings");
        }

        public static List<Listing> PrepareResponse(List<Listing> Listings)
        {
            var Result = new List<Listing>();
            var Duplicates = Listings.Take(400).Select((x, index) => new { x, index, x.Company, x.ListingId, x.Image_List, x.CategoryId }).ToLookup(x => x.Company);
            foreach (var Duplicate in Duplicates.Where(x => x.Count() > 1))
            {
                var start = 0;
                foreach (var item in Duplicate)
                {
                    Result.Add(new Listing
                    {
                        Company = $"{item.Company}{(start > 0 ? " " + start : "")}",
                        ListingId = item.ListingId,
                        Image_List = item.Image_List
                    });
                    start++;
                }
            }
            return Result;
        }

        public static List<Listing> ComplementResult(List<Listing> Listings, int Limit)
        {
            try
            {
                while (Listings.Count < Limit)
                {
                    Listings.AddRange(Listings);
                }
                return Listings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
