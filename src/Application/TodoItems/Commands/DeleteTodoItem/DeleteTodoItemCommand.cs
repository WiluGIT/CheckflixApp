﻿using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Domain.Entities;
using CheckflixApp.Domain.Events;
using MediatR;

namespace CheckflixApp.Application.TodoItems.Commands.DeleteTodoItem;
public record DeleteTodoItemCommand(int Id) : IRequest<Unit>;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<TodoItem>()
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        _context.Set<TodoItem>().Remove(entity);

        entity.AddDomainEvent(new TodoItemDeletedEvent(entity));

        //await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
