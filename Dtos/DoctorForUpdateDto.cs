using System.ComponentModel.DataAnnotations;

namespace ProjectTest.Dtos
{
    public class DoctorForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        [Required]
        public string Extra_Info { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int SpeciaizeId { get; set; }
    }
}
