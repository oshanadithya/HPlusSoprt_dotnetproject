using HPlusSportAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        //Synchronus  GET all method
        /*[HttpGet]
        public ActionResult GetAllProducts()
        {
            return Ok(_context.Products.ToArray());
        }*/

        //Asynchronus  GET all method
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            return Ok(_context.Products.ToArrayAsync());
        }

        //Synchronus  get from id method
        /* [HttpGet("{id}")]
         public ActionResult GetProduct(int id)
         {
             var product = _context.Products.Find(id);
             if (product == null)
             {
                 return NotFound();
             }
             return Ok(product);
         }*/

        //Asynchronus  GET from Id method
        [HttpGet("{id}")]
        public async Task <ActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //Asynchronus  POST method
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetProduct",
                new { id = product.Id },
                product
            );
        }

        //Asynchronus PUT method
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                if (!_context.Products.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }

            return NoContent();
        }

        //Asynchronus DELETE method
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        //Asynchronus Multiple DELETE method
        [HttpPost]
        [Route("Delete")]
        public async Task<ActionResult<Product>> DeleteMultipleProduct([FromQuery] int[] ids)
        {
            var products = new List<Product>();
            foreach (var id in ids)
            {
                var product = await _context.Products.FindAsync(ids);
                if (product == null)
                {
                    return NotFound();
                }

                products.Add(product);
            } 

            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();

            return Ok(products);
        }
    }
}