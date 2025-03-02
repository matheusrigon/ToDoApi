using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests;

[TestClass]
public class CreateTodoHandlerTests
{
    private readonly CreateTodoCommand _invalidCommand = new("1", "1", DateTime.Now);
    private readonly CreateTodoCommand _validCommand = new("Tarefa teste", "Matheus", DateTime.Now);
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
}