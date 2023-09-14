namespace UfinetPrueba.Domain.Entities
{
    public class Hotel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int stars { get; set; }

        public ICollection<Countries> Countries { get; set; }
    }
}
