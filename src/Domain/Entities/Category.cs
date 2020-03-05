using Golobal_IMC_Task.Domain.Common;
using System.Collections.Generic;

namespace Golobal_IMC_Task.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }

        public string CategoryName { get; set; }

        public IList<Product> Products { get; set; }
    }
}
