using Golobal_IMC_Task.Application.Categorys.Queries.ExportCategorys;
using CsvHelper.Configuration;

namespace Golobal_IMC_Task.Infrastructure.Files.Maps
{
    public class ProductRecordMap : ClassMap<ProductRecord>
    {
        public ProductRecordMap()
        {
            AutoMap();
            Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
        }
    }
}
