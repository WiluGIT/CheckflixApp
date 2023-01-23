using System.Globalization;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.TodoLists.Queries.ExportTodos;
using CheckflixApp.Infrastructure.Files.Maps;
using CsvHelper;

namespace CheckflixApp.Infrastructure.Files;
public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
