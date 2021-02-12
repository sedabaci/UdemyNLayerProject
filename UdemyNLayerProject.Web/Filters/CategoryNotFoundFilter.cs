using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Web.DTOS;

namespace UdemyNLayerProject.Web.Filters
{
    public class CategoryNotFoundFilter : ActionFilterAttribute
    {
        private readonly ICategoryService _categoryService;
        public CategoryNotFoundFilter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var category = await _categoryService.GetByIdAsync(id);

            if (category != null)
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Errors.Add($"Category with id {id} not found in database!");
                context.Result = new RedirectToActionResult("Error","Home",errorDto);
            }
        }
    }
}
