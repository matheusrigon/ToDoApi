using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.Tests.Repositories;

public class FakeTodoRepository : ITodoRepository
{
    public void Create(TodoItem todo)
    {
    }   

    public void Update(TodoItem todo)
    {
    }

    public TodoItem GetById(Guid id, string user)        
        => new("Titulo da tarefa", "Matheus", DateTime.Now);

    public IEnumerable<TodoItem> GetAll(string user)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TodoItem> GetAllDone(string user)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TodoItem> GetAllUndone(string user)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TodoItem> GetAllByPeriod(string user, DateTime date, bool done)
    {
        throw new NotImplementedException();
    }

    public TodoItem? GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}
