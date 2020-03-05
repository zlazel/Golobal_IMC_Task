using Golobal_IMC_Task.Application.Common.Mappings;
using Golobal_IMC_Task.Domain.Entities;
using System.Collections.Generic;

namespace Golobal_IMC_Task.Application.Categorys.Queries.GetCategorys
{
    public class CategoryDto : IMapFrom<Category>
{
    public CategoryDto()
    {
        Products = new List<ProductDto>();
    }

    public int Id { get; set; }

    public string CategoryName { get; set; }

    public IList<ProductDto> Products { get; set; }
}
}
