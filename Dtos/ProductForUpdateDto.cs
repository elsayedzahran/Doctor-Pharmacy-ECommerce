using ProjectTest.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectTest.Dtos
{
    public class ProductForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
