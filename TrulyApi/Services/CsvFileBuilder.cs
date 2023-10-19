using CsvHelper;
using System.Globalization;
using TrulyApi.Dtos.Card;
using TrulyApi.Dtos.Quote;

namespace TrulyApi.Services
{

    public interface IICsvFileBuilder
    {
        byte[] BuildQuoteItemsFile (IEnumerable<ExportQuoteItemRecord> records);
        byte[] BuilCardItemsFile(IEnumerable<ExportCardItemRecord> records);
    }
    public class CsvFileBuilder : IICsvFileBuilder
    {
        public byte[] BuildQuoteItemsFile(IEnumerable<ExportQuoteItemRecord> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                //csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }

        public byte[] BuilCardItemsFile(IEnumerable<ExportCardItemRecord> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                //csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
    }
}
