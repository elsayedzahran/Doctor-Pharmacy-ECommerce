using System.ComponentModel.DataAnnotations;

namespace ProjectTest.Dtos
{
    public class SellerForRegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        //[EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public int CityId { get; set; }
    }
}
