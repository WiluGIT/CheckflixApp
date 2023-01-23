using System.Globalization;
using CheckflixApp.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace CheckflixApp.Infrastructure.Files.Maps;
public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
    }
}
