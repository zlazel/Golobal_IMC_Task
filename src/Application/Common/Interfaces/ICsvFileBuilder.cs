using Golobal_IMC_Task.Application.Categorys.Queries.ExportCategorys;
using System.Collections.Generic;

namespace Golobal_IMC_Task.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildProductsFile(IEnumerable<ProductRecord> records);
    }
}
