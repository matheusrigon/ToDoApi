using Todo.Domain.Entities;

namespace Todo.Domain.Tests.EntityTests;

[TestClass]
public class TodoItemTests()
{
    private readonly TodoItem _validTodo = new("Titulo aqui", "Matheus", DateTime.Now);

    [TestMethod]
    public void DadoUmNovoTodoOMesmoNaoPodeSerConcluido()
    {
        Assert.AreEqual(_validTodo.Done, false);
    }
}
