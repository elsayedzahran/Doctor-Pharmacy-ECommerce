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
    [Authorize(Roles = "Seller")]
    public class SellerOperationsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<UserEntity> userManager;

        public SellerOperationsController(ApplicationDbContext context,
            UserManager<UserEntity> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        /*
         * todo 
         * search
         */


        [HttpGet("Get-All-Products-for-specific-user")]
        public async Task<IActionResult> getAll()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userId == Guid.Empty)
                return Unauthorized("you are unauthorized");
            var products =await context.Products.Where( p => p.UserId == userId).ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductForCreateDto Product)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userId == Guid.Empty) 
                return Unauthorized("you are unauthorized");
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return NotFound();
            var product = new ProductEntity()
            {
                Name= Product.Name,
                Price= Product.Price,
                Description= Product.Description,
                CategoryId= Product.CategoryId,
                UserId= userId,
                CityId = (int)user.CityId
            };
            context.Add(product);
            await context.SaveChangesAsync();
            return Ok(Product);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int ProductId, ProductForUpdateDto productForUpdate)
        {
            var product = await context.Products.FindAsync(ProductId);
            if (product == null) 
                return NotFound();
            var userId = product.UserId;
            if (userId == Guid.Empty || userId != productForUpdate.UserId)
                return Unauthorized();
            product.Name = productForUpdate.Name;
            product.Price = productForUpdate.Price;
            product.Description = productForUpdate.Description;
            product.CategoryId = productForUpdate.CategoryId;
            await context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> RemoveProduct(int ProductId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userId == Guid.Empty)
                return BadRequest("can't find user Id");

            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return BadRequest($"can't find user with the id = {userId}");

            var delete_Product = await context.Products.FindAsync(ProductId);

            if (delete_Product == null)
                return BadRequest();

            context.Remove(delete_Product);
            await context.SaveChangesAsync();
            return Ok(delete_Product);
        }
    }
}
