using CheckflixApp.Application.Common.Mappings;
using CheckflixApp.Domain.Entities;

namespace CheckflixApp.Application.TodoLists.Queries.ExportTodos;
public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
