using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.TodoLists.Commands.CreateTodoList;
using CheckflixApp.Application.TodoLists.Commands.DeleteTodoList;
using CheckflixApp.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

using static CheckflixApp.Application.IntegrationTests.Testing;

namespace CheckflixApp.Application.IntegrationTests.TodoLists.Commands;
public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
