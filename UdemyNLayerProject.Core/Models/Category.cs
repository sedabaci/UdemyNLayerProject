using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UdemyNLayerProject.Core.Models
{
    public class Category
    {
        //code first: burda olusturulan modellerin db'de tabloya dönüştürülmesi
        //db first:   db'de ki tabloların burada modele cevrilmesi.
        public Category()
        {
            Products = new Collection<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Product> Products { get; set; } 
        // bire cok ilişki var. her category nin birden cok productı olabilir. bos bir collection dizisi olusturduk. 
    }
}
