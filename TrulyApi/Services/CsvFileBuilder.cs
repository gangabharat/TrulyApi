using CsvHelper;
using System.Globalization;
using TrulyApi.Dtos.Card;
using TrulyApi.Dtos.Quote;

namespace TrulyApi.Services
{

    public interface ICsvFileBuilder<T>
    {
        byte[] BuildQuoteItemsFile(IEnumerable<ExportQuoteItemRecord> records);
        byte[] BuilCardItemsFile(IEnumerable<ExportCardItemRecord> records);
        byte[] BuildFile(IEnumerable<T> records);
    }
    public class CsvFileBuilder<T> : ICsvFileBuilder<T>
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


        public byte[] BuildFile(IEnumerable<T> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                //csvWriter.WriteRecords(records);
                csvWriter.WriteRecord(records);
                //csvWriter.rea
            }

            return memoryStream.ToArray();
        }
    }
}
