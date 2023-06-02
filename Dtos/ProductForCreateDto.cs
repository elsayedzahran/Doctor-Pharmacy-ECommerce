using System.ComponentModel.DataAnnotations;

namespace ProjectTest.Dtos
{
    public class ProductForCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
