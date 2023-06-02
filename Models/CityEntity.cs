namespace ProjectTest.Models
{
    public class CityEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public GovernateEntity Governate { get; set; }
        public int GovernateId { get; set; }
        public List<ProductEntity> Products { get; set; }
    }
}