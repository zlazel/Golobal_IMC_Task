using Golobal_IMC_Task.Application.Common.Interfaces;
using Golobal_IMC_Task.Application.Categorys.Queries.ExportCategorys;
using Golobal_IMC_Task.Infrastructure.Files.Maps;
using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace Golobal_IMC_Task.Infrastructure.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildProductsFile(IEnumerable<ProductRecord> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);

                csvWriter.Configuration.RegisterClassMap<ProductRecordMap>();
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
    }
}
