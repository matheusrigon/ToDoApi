using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers;

public class TodoHandler : 
    Notifiable<Notification>,
    IHandler<CreateTodoCommand>,
    IHandler<UpdateTodoCommand>,
    IHandler<MarkTodoAsDoneCommand>,
    IHandler<MarkTodoAsUndoneCommand>
{
    private readonly ITodoRepository _repository;

    public TodoHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public ICommandResult Handle(CreateTodoCommand command)
    {
        // Fail Fast Validation
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(false, "Parece que sua tarefa está errada!", command.Notifications);

        // Cria TodoItem
        TodoItem todo = new(command.Title, command.User, command.Date);

        // Salva no banco
        _repository.Create(todo);

        // Notifica usuário
        return new GenericCommandResult(true, "Tarefa salva!", todo);
    }

    public ICommandResult Handle(UpdateTodoCommand command)
    {
        //Fail Fast Validation
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(false, "Parece que sua tarefa está errada!", command.Notifications);

        // Recupera TodoItem do banco (rehidratação)
        var todo = _repository.GetById(command.Id);

        // Altera o título
        todo.UpdateTitle(command.Title);

        // Salva no banco
        _repository.Update(todo);

        // Notifica usuário
        return new GenericCommandResult(true, "Tarefa atualizada!", todo);
    }

    public ICommandResult Handle(MarkTodoAsDoneCommand command)
    {
        //Fail Fast Validation
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(false, "Parece que sua tarefa está errada!", command.Notifications);

        // Recupera TodoItem do banco
        var todo = _repository.GetById(command.Id);

        // Altera o estado
        todo.MarkAsDone();

        // Salva no banco
        _repository.Update(todo);

        // Retorna resultado
        return new GenericCommandResult(true, "Status da tarefa atualizado!", todo);
    }

    public ICommandResult Handle(MarkTodoAsUndoneCommand command)
    {
        //Fail Fast Validation
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(false, "Parece que sua tarefa está errada!", command.Notifications);

        // Recupera TodoItem do banco
        var todo = _repository.GetById(command.Id);

        // Altera o estado
        todo.MarkAsUndone();

        // Salva no banco
        _repository.Update(todo);

        // Retorna resultado
        return new GenericCommandResult(true, "Status da tarefa atualizado!", todo);
    }
}
