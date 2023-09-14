namespace UfinetPrueba.Domain.Entities
{
    public class Countries
    {

        public int id { get; set; }
        public string name { get; set; }
        public string isocode { get; set; }
        public int population { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }
}
