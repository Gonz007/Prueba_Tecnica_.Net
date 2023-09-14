namespace UfinetPrueba.Application.Features.Information
{
    public class GetInfoDto
    {
        public int rowsPerPage{ get; set; }
        public int pageNumber { get; set; }
        public Filters? Filter { get; set; }
    }
    public class Filters
    {
        public string? NameCountries{ get; set; }
        public string? isocode { get; set; }
        public string? NameRestaurant { get; set; }
        public string? NameHotel { get; set; }

    }
}
