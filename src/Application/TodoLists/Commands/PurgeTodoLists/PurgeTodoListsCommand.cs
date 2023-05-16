using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Common.Security;
using CheckflixApp.Domain.Entities;
using MediatR;

namespace CheckflixApp.Application.TodoLists.Commands.PurgeTodoLists;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public record PurgeTodoListsCommand : IRequest<Unit>;

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public PurgeTodoListsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        _context.Set<TodoList>().RemoveRange(_context.Set<TodoList>());

        //await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
