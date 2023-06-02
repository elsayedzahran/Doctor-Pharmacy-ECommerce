namespace ProjectTest.Models
{
    public class DoctorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Price { get; set; }
        public bool? CanGoHome { get; set; }
        public string? Extra_Info { get; set; }
        public SpecializeEntity? Specialize { get; set; }
        public int? SpeciaizeId { get; set; }
        public CityEntity? City { get; set; }
        public int? CityId { get; set; }
        public Guid? UserId { get; set; }
    }
}
