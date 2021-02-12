using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Web.DTOS;

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
    }
}
