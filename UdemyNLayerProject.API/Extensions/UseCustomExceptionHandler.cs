using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using UdemyNLayerProject.API.DTOS;

namespace UdemyNLayerProject.API.Extensions
{
    public static class UseCustomExceptionHandler
    {
        /// <summary>
        /// IApplicationBuilder üzerine custom extension method yazdık, exceptionları global olarak ele almak için
        /// Extension methodlar mutlaka static class ve static method olmalı
        /// </summary>
        /// <param name="app"></param>
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var ex = error.Error;
                        ErrorDto errorDto = new ErrorDto();

                        errorDto.Status = 500;
                        errorDto.Errors.Add(ex.Message);

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto));   //WriteAsync methodu kullandığımız için manuel convert gerceklestirmek gerekti.
                    }
                });
            });
        }

    }
}
