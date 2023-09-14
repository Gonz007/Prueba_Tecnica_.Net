namespace UfinetPrueba.Application.Features.Information
{
    public class GetInfoResponseDto
    {
        public int Total { get; set; }

        public List<GetInfoDetail> GetInfoDetail { get; set; }
    }
    public class GetInfoDetail
    {
        public int id { get; set; }
        public string name { get; set; }
        public int population { get; set; }
        public string isocode { get; set; }
        public List<GetRestaurantDetail> GetRestaurantDetail { get; set; }
        public List<GetHotelDetail> GetHotelDetail { get; set; }
    }
    public class GetRestaurantDetail
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
    public class GetHotelDetail
    {
        public int id { get; set; }
        public string name { get; set; }
        public int stars { get; set; }
    }
}
