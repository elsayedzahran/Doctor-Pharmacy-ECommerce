namespace ProjectTest.Models
{
    public class GovernateEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CityEntity> City { get; set; }
    }
}