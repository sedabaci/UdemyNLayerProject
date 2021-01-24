using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Core.Services
{
    public interface IProductService : IService<Product>
    {
        //db ile ilgili olmayan, Product nesnesi üzerinden iç kontroller olacaksa, bu katmanda tanımlanır.

        // bool ControlInnerBarkod(Product product);
        Task<Product> GetWithCategoryByIdAsync(int productId);


    }
}
