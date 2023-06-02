using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTest.Models;

namespace ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientOperationsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ClientOperationsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("Get-All-Products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await context.Products.ToListAsync();
            return Ok(products);
        }
        [HttpGet("Get-All-Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await context.Categories.Include( c => c.Products).ToListAsync();
            return Ok(categories);
        }
        [HttpGet("Get-Product-By-Id")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await context.Products.FirstOrDefaultAsync(opt => opt.Id == productId);
            if (product == null)
                return BadRequest("Invalid Product Id");

            return Ok(product);
        }
        [HttpGet("Get-Products-By-CityId")]
        public async Task<IActionResult> GetProductsForCity([FromBody] int CityId)
        {
            var city_exist = await context.Cities.FindAsync(CityId);
            if (city_exist == null)
                return BadRequest("Invalid CityID");
            var products = await context.Products.Where(p => p.CityId == CityId).ToListAsync();
            return Ok(products);
        }
        [HttpGet("Get-Products-By-CategoryId")]
        public async Task<IActionResult> GetProductsForCategory([FromBody]int categoryId)
        {
            var category_exist = await context.Categories.FindAsync(categoryId);
            if (category_exist == null)
                return BadRequest("Invalid CategoryID");
            var products = await context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
            return Ok(products);
        }

        [HttpGet("Get-Doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await context.Doctors.ToListAsync();  
            return Ok(doctors);
        }

        [HttpGet("search-products")]
        public ActionResult<IEnumerable<ProductEntity>> SearchProducts(string name, string category, decimal? minPrice, decimal? maxPrice)
        {
            var query = context.Products.AsQueryable();

            // Apply filters based on the provided parameters
            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.Name.ToLower() == category.ToLower());

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            var products = query.ToList();

            return Ok(products);
        }


        [HttpGet("Doctors search")]
        public ActionResult<IEnumerable<DoctorEntity>> SearchDoctors(string? name, string? address, decimal? minPrice, decimal? maxPrice)
        {
            var query = context.Doctors.AsQueryable();

            // Apply filters based on the provided parameters
            if (!string.IsNullOrEmpty(name))
                query = query.Where(d => d.Name.ToLower().Contains(name.ToLower()));

            if (!string.IsNullOrEmpty(address))
                query = query.Where(d => d.Address != null && d.Address.ToLower().Contains(address.ToLower()));

            if (minPrice.HasValue)
                query = query.Where(d => d.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(d => d.Price <= maxPrice.Value);

            var doctors = query.ToList();

            return Ok(doctors);
        }


    }
}
