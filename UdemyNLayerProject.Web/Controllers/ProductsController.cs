using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ProductApiService _productApiService;
        private readonly IMapper _mapper;
        public ProductsController(IMapper mapper, ProductApiService productApiService)
        {
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
            var product = await _productApiService.GetByIdAsync(id);
            return View(_mapper.Map<ProductDto>(product));
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            await _productApiService.Update(productDto);
            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(ProductNotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            await _productApiService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
