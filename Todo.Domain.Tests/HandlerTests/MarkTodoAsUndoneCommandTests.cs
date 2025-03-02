using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests;

[TestClass]
public class MarkTodoAsUndoneCommandTests
{
    private readonly MarkTodoAsUndoneCommand _invalidCommand = new(new Guid(), "Matheus");
    private readonly MarkTodoAsUndoneCommand _validCommand = new(new Guid(), "Mat");
    private readonly TodoHandler _handler = new(new FakeTodoRepository());

    [TestMethod]
    public void DadoUmComandoInvalidoDeveInterromperAExecucao()
    {
        var result = (GenericCommandResult)_handler.Handle(_invalidCommand);

        Assert.AreEqual(result.Success, false);
    }

    [TestMethod]
    public void DadoUmComandoInvalidoDeveCriarATarefa()
    {
        var result = (GenericCommandResult)_handler.Handle(_validCommand);

        Assert.AreEqual(result.Success, true);
    }

    [TestMethod]
    public void DadoUmComandoValidoAlterarStatusTodoItem()
    {
        var result = (GenericCommandResult)_handler.Handle(_validCommand);

        Assert.AreEqual(((TodoItem)result.Data).Done, false);
    }
}