﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOS;
using UdemyNLayerProject.Core.Services;

namespace UdemyNLayerProject.API.Filters
{
    public class ProductNotFoundFilter : ActionFilterAttribute
    {
        private readonly IProductService _productService; 
        //Filterımız dependency injection nesnesi alıyorsa, önce startup.cs içerisine addScoped olarak ekliyoruz.
        public ProductNotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var product = await _productService.GetByIdAsync(id);

            if (product != null)
            {
                await next(); //product null değilse requesti devam ettirir
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Status = 404;
                errorDto.Errors.Add($"id'si {id} olan ürün veritabanında bulunamadı.");
                context.Result = new NotFoundObjectResult(errorDto);
            }
        }
    }
}
