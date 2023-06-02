using System.ComponentModel.DataAnnotations;

namespace ProjectTest.Dtos
{
    public class ClientForRegisterDto
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
    }
}
