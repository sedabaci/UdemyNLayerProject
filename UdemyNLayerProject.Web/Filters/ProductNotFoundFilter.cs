using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.ApiService;
using UdemyNLayerProject.Web.DTOS;

namespace UdemyNLayerProject.Web.Filters
{
    public class ProductNotFoundFilter : ActionFilterAttribute
    {
        private readonly ProductApiService productApiService; 
        //Filterımız dependency injection nesnesi alıyorsa, önce startup.cs içerisine addScoped olarak ekliyoruz.
        public ProductNotFoundFilter(ProductApiService productService)
        {
            productApiService = productService;
        }
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var product = await productApiService.GetByIdAsync(id);

            if (product != null)
            {
                await next(); //product null değilse requesti devam ettirir
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Errors.Add($"Product with id {id} not found in database!");
                context.Result = new RedirectToActionResult("Error", "Home", errorDto);
            }
        }
    }
}
