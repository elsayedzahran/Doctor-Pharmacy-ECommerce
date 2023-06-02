using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTest.Dtos;
using ProjectTest.Models;
using System.Security.Claims;

namespace ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorOperationsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<UserEntity> userManager;

        public DoctorOperationsController(ApplicationDbContext context, UserManager<UserEntity> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInfo(DoctorForUpdateDto doctorForUpdate)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userId == Guid.Empty)
                return Unauthorized("you are unauthorized");
            var doctor =await context.Doctors.FirstOrDefaultAsync( d => d.UserId == userId);
            doctor.Address = doctorForUpdate.Address;
            doctor.Price = doctorForUpdate.Price;
            doctor.Name = doctorForUpdate.Name;
            doctor.Extra_Info = doctorForUpdate.Extra_Info;
            doctor.PhoneNumber = doctorForUpdate.Phone;
            doctor.CityId = doctorForUpdate.CityId;
            doctor.SpeciaizeId = doctorForUpdate.SpeciaizeId;
            await context.SaveChangesAsync();
            return Ok(doctor);

        }
    }
}
