using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTests;

[TestClass]
public class TodoQueriesTests
{
    private List<TodoItem> _items;

    public TodoQueriesTests()
    {
        _items = [];

        var item1Done = new TodoItem("Tarefa 1", "Matheus", DateTime.Now);
        var item2Done = new TodoItem("Tarefa 3", "Matheus", DateTime.Now.AddDays(-1));

        item1Done.MarkAsDone();
        item2Done.MarkAsDone();

        _items.Add(item1Done);
        _items.Add(item2Done);
        _items.Add(new TodoItem("Tarefa 2", "Teste", DateTime.Now));
        _items.Add(new TodoItem("Tarefa 4", "Teste1", DateTime.Now.AddDays(-1)));
        _items.Add(new TodoItem("Tarefa 5", "Matheus", DateTime.Now.AddDays(-1)));
    }

    [TestMethod]
    public void DadaAConsultaDeveRetornarTarefasApenasDoUsuarioMatheus()
    {
        var result = _items.AsQueryable().Where(TodoQueries.GetAll("Matheus"));

        Assert.AreEqual(3, result.Count());
    }

    [TestMethod]
    public void DadaAConsultaDeveRetornarTarefasApenasDoUsuarioMatheusMarcadasComoDone()
    {
        var result = _items.AsQueryable().Where(TodoQueries.GetAllDone("Matheus"));

        Assert.AreEqual(2, result.Where(x => x.Done).Count());
    }

    [TestMethod]
    public void DadaAConsultaDeveRetornarTarefasApenasDoUsuarioMatheusMarcadasComoUndone()
    {
        var result = _items.AsQueryable().Where(TodoQueries.GetAllUndone("Matheus"));

        Assert.AreEqual(1, result.Where(x => !x.Done).Count());
    }

    [TestMethod]
    public void DadaAConsultaDeveRetornarTarefasApenasDoUsuarioMatheusMarcadasComoDonePorPeriodo()
    {
        var result = _items.AsQueryable().Where(TodoQueries.GetAllByPeriod("Matheus", DateTime.Now.AddDays(-1), true));

        Assert.AreEqual(1, result.Where(x => x.Done).Count());
    }
}