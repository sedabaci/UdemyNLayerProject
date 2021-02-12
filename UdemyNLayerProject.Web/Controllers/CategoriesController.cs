﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UdemyNLayerProject.Core.Services;

namespace UdemyNLayerProject.Web.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper; 
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
