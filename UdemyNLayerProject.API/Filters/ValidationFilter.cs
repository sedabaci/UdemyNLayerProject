﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using UdemyNLayerProject.API.DTOS;

namespace UdemyNLayerProject.API.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorDto errorDto = new ErrorDto();

                errorDto.Status = 400;

                IEnumerable<ModelError> modelError = context.ModelState.Values.SelectMany(v => v.Errors);

                modelError.ToList().ForEach(x =>
                {
                    errorDto.Errors.Add(x.ErrorMessage);
                });
                context.Result = new BadRequestObjectResult(errorDto);
            };
        }
    }
}
