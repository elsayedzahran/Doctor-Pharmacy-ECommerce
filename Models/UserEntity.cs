using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTest.Models
{
    public class UserEntity : IdentityUser
    {
        public List<ProductEntity>? Products { get; set; }
        public CityEntity City { get; set; }
        public int? CityId { get; set;}

    }
}