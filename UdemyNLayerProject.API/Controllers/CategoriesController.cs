using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOS;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;

namespace UdemyNLayerProject.API.Controllers
{
    /// <summary>
    /// action methodlar
    /// HTTP Status 200:OK, 
    /// HTTP Status 201:Created,
    /// HTPP Status 204:NoContent,
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        /// <summary>
        /// Get category by id with products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProductsById(int id)
        {
            var category = await _categoryService.GetWithProductsByIdAsync(id);
            return Ok(_mapper.Map<CategoryWithProductDto>(category));
        }

        /// <summary>
        /// Save a category
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var newCategory = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto)); //client'dan aldığımız categoryDto'yu Category'e dönüştürdük
            return Created(string.Empty, _mapper.Map<CategoryDto>(newCategory));
        }
        /// <summary>
        /// Update the category
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(CategoryDto categoryDto)
        {
            var updateCategory = _categoryService.Update(_mapper.Map<Category>(categoryDto));
            return NoContent(); // upd için geriye bir sey dönmüyorum(204) ,best practice 
        }
        /// <summary>
        /// Delete the category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;  //asycn-await kullanmadan aycn method cağırdık.
            _categoryService.Remove(category);
            return NoContent(); // del için geriye bir sey dönmüyorum(204) ,best practice 
        }
    }
}
