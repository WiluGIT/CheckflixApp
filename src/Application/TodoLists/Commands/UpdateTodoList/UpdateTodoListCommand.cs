using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Entities;
using MediatR;

namespace CheckflixApp.Application.TodoLists.Commands.UpdateTodoList;
public record UpdateTodoListCommand : IRequest<Unit>
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<TodoList>()
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        entity.Title = request.Title;

        //await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
