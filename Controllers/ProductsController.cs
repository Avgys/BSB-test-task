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
using Microsoft.AspNetCore.Authorization;
using Catalog.DTO;
using System.Security.Claims;

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
            if (User.IsInRole("admin") || User.IsInRole("user"))
            {
                return Ok(_mapper.Map<IEnumerable<ProductAuthDTO>>(products));
            }
            else
            {
                return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));
            }
        }

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

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDTO);
            var updatedProduct = await _productRepo.UpdateAsync(product);
            if (updatedProduct != null)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
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
                await _categoryRepo.SaveChangesAsync();
            }

            newProduct.CategoryId = category.Id;
            await _productRepo.AddAsync(newProduct);
            await _productRepo.SaveChangesAsync();

            ProductDTO productDTO = _mapper.Map<ProductDTO>(newProduct);
            return CreatedAtAction("GetProduct", new { id = productDTO.Id }, productDTO);
        }
                
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (_productRepo.Delete(product))
            {
                await _productRepo.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
