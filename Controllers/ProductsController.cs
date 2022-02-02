using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalog.Data;
using Catalog.Models;
using AutoMapper;

namespace BSB_test_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepo productRepo, ICategoryRepo categoryRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts([FromQuery] string name)
        {
            IEnumerable<Product> products;
            if (name == null)
            {
                products = await _productRepo.GetAllAsync();
            }
            else
            {
                products = await _productRepo.GetAllAsync(name);
            }
            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDTO>(product));
        }

        //// PUT: api/Products/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProduct(int id, Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //POST: api/Products
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDto)
        {
            Product newProduct = _mapper.Map<Product>(productDto);

            var category = await _categoryRepo.GetByNameAsync(productDto.CategoryName);
            if (category == null)
            {
                category = new Category() {
                    Name = productDto.CategoryName
                };
                await _categoryRepo.AddAsync(category);
            }

            newProduct.CategoryId = category.Id;
            await _productRepo.AddAsync(newProduct);
            await _productRepo.SaveChangesAsync();

            ProductDTO productDTO = _mapper.Map<ProductDTO>(newProduct);
            return CreatedAtAction("GetProduct", new { id = productDTO.Id }, productDTO);
        }

        //// DELETE: api/Products/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
