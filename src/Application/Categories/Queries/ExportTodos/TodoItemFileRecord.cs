using Golobal_IMC_Task.Application.Common.Mappings;
using Golobal_IMC_Task.Domain.Entities;

namespace Golobal_IMC_Task.Application.Categorys.Queries.ExportCategorys
{
    public class ProductRecord : IMapFrom<Product>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
