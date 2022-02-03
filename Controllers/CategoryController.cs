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
using Catalog.DTO;
using Microsoft.AspNetCore.Authorization;

namespace BSB_test_task.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest();
            }

            var category = _mapper.Map<Category>(categoryDTO);
            var updatedCategory = await _categoryRepo.UpdateAsync(category);
            if (updatedCategory != null)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryDTO)
        {
            if (_categoryRepo.GetByNameAsync(categoryDTO.Name).Result == null)
            {
                var category = _mapper.Map<Category>(categoryDTO);
                await _categoryRepo.AddAsync(category);
                await _categoryRepo.SaveChangesAsync();
                categoryDTO = _mapper.Map<CategoryDTO>(category);
                return CreatedAtAction("GetCatgory", new { id = categoryDTO.Id }, categoryDTO);
            }
            else
            {                
                return Conflict("Resourse already exists");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            if (_categoryRepo.Delete(category))
            {
                await _categoryRepo.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }        
    }
}
