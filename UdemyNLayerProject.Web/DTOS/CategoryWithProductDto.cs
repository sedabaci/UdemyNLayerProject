using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyNLayerProject.Web.DTOS
{
    public class CategoryWithProductDto : CategoryDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
