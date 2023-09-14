namespace UfinetPrueba.Domain.Entities
{
    public class Restaurant
    {

        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }

        public ICollection<Countries> Countries { get; set; }
    }
}
