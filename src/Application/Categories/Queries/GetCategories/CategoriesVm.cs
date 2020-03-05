using Golobal_IMC_Task.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Golobal_IMC_Task.Application.Categorys.Queries.GetCategorys
{
    public class CategoriesVm
    {
        public IList<CategoryDto> Categories { get; set; }
    }
}
