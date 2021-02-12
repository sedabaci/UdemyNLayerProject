using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Web.DTOS;
using UdemyNLayerProject.Web.Filters;

namespace UdemyNLayerProject.Web.Controllers
{
    public class CategoriesController : Controller
    {
        /// <summary>
        /// index sayfasında categorylerimi görüntülemek istiyorum
        /// </summary>
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
        public IActionResult Create()
        {
            return View(); //sayfa yüklendiğinde ilk calısacak methodum
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            //sayfa yüklendiği an client'dan aldığım id'ye ait categori bilgilerini alıyorum
            var category = await _categoryService.GetByIdAsync(id);
            return View(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public IActionResult Update(CategoryDto categoryDto)
        {
            _categoryService.Update(_mapper.Map<Category>(categoryDto));            
            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(CategoryNotFoundFilter))]  //Filter içerisinde DI geçtiği için ServiceFilter ile tanımladık
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);
            return RedirectToAction("Index");
        }

    }
}
