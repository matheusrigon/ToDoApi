using Todo.Domain.Commands;

namespace Todo.Domain.Tests.CommandTests;

[TestClass]
public class CreateTodoCommandTests
{
    private readonly CreateTodoCommand _invalidCommand = new("1", "1", DateTime.Now);
    private readonly CreateTodoCommand _validCommand = new("Criar model", "matheus", DateTime.Now);

    public CreateTodoCommandTests()
    {
        _invalidCommand.Validate();
        _validCommand.Validate();
    }

    [TestMethod]
    public void DadoUmComandoInvalido()
    {
        Assert.AreEqual(_invalidCommand.IsValid, false);
    }

    [TestMethod]
    public void DadoUmComandoValido()
    {
        Assert.AreEqual(_validCommand.IsValid, true);
    }
}