using CheckflixApp.Application.TodoLists.Queries.ExportTodos;

namespace CheckflixApp.Application.Common.Interfaces;
public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
