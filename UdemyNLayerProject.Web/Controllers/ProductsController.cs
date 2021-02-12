using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Web.DTOS;

namespace UdemyNLayerProject.Web.Controllers
{
    /// <summary>
    /// index sayfasında Productlarımı görüntülemek istiyorum.
    /// </summary>
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<ProductDto>>(products));
        }
    }
}
