using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTest.Models
{
    public class ProductEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price  { get; set; }
        public UserEntity User { get; set; }
        public Guid UserId { get; set; }
        public CategoryEntity? Category { get; set; }
        public int CategoryId { get; set; }
        public CityEntity? City { get; set; }
        public int CityId { get; set; }
    }
}