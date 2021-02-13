using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Web.DTOS;
using UdemyNLayerProject.Web.Filters;

namespace UdemyNLayerProject.Web.Controllers
{
    /// <summary>
    /// index sayfasında Productlarımı görüntülemek istiyorum.
    /// Web projesi, API ile haberleşiyor
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
        public IActionResult Create()
        {
            //sayfa yüklendiğinde cağırılacak
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var newProduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            //sayfa yüklendiği an client'dan aldığım id'ye ait product bilgilerini alıyorum
            var product = await _productService.GetByIdAsync(id);
            return View(_mapper.Map<ProductDto>(product));
        }
        [HttpPost]
        public IActionResult Update(ProductDto productDto)
        {
            _productService.Update(_mapper.Map<Product>(productDto));
            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(ProductNotFoundFilter))]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;
            _productService.Remove(product);
            return RedirectToAction("Index");
        }
    }
}
