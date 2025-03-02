using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Queries;
using Todo.Domain.Repositories;

namespace Todo.Domain.Infra.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly DataContext _context;

    public TodoRepository(DataContext context)
    {
        _context = context;
    }

    public void Create(TodoItem todo)
    {
        _context.Todos.Add(todo);
        _context.SaveChanges();
    }

    public IEnumerable<TodoItem> GetAll(string user)    
        => _context.Todos
                .AsNoTracking()
                .Where(TodoQueries.GetAll(user))
                .OrderBy(x => x.Date);    

    public IEnumerable<TodoItem> GetAllByPeriod(string user, DateTime date, bool done)    
        => _context.Todos
                .AsNoTracking()
                .Where(TodoQueries.GetAllByPeriod(user, date, done))
                .OrderBy(x => x.Date);
    

    public IEnumerable<TodoItem> GetAllDone(string user)
        => _context.Todos
                .AsNoTracking()
                .Where(TodoQueries.GetAllDone(user))
                .OrderBy(x => x.Date);

    public IEnumerable<TodoItem> GetAllUndone(string user)
        => _context.Todos
                .AsNoTracking()
                .Where(TodoQueries.GetAllUndone(user))
                .OrderBy(x => x.Date);

    public TodoItem? GetById(Guid id)
        => _context.Todos
                .AsNoTracking()
                .FirstOrDefault(TodoQueries.GetById(id));

    public void Update(TodoItem todo)
    {
        _context.Entry(todo).State = EntityState.Modified;
        _context.SaveChanges();
    }    
}