using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Web.ApiService;
using UdemyNLayerProject.Web.DTOS;
using UdemyNLayerProject.Web.Filters;

namespace UdemyNLayerProject.Web.Controllers
{
    /// <summary>
    /// index sayfasında Productlarımı görüntülemek istiyorum.
    /// Client, API ile haberleşiyor
    /// </summary>
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ProductApiService _productApiService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper, ProductApiService productApiService)
        {
            _productService = productService;
            _productApiService = productApiService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productApiService.GetAllAsync();
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
            await _productApiService.AddAsync(productDto);
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
