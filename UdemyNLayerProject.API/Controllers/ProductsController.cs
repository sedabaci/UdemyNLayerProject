using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOS;
using UdemyNLayerProject.API.Filters;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(await _productService.GetAllAsync()));
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(ProductNotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<ProductDto>(await _productService.GetByIdAsync(id)));
        }

        /// <summary>
        /// Get products by id with categories
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(ProductNotFoundFilter))]
        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryById(int id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);
            return Ok(_mapper.Map<ProductWithCategoryDto>(product));
        }

        /// <summary>
        /// Save a category
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>

        //[ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var newProduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            return Created(string.Empty, _mapper.Map<ProductDto>(newProduct));
        }
        /// <summary>
        /// Update the products
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(ProductDto productDto)
        {
            var updateProduct = _productService.Update(_mapper.Map<Product>(productDto));
            return NoContent();
        }

        /// <summary>
        /// Delete the product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(ProductNotFoundFilter))]
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var deleteProduct = _productService.GetByIdAsync(id).Result;
            _productService.Remove(deleteProduct);
            return NoContent();
        }


    }
}
