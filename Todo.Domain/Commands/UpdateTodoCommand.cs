using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands;

public class UpdateTodoCommand : Notifiable<Notification>, ICommand
{
    public UpdateTodoCommand()
    {
    }

    public UpdateTodoCommand(string title, string user, DateTime date)
    {
        Title = title;
        User = user;
        Date = date;
    }

    public Guid Id {get;set;}
    public string Title {get;set;}
    public string User {get;set;}
    public DateTime Date {get; set;}

    public void Validate() 
        => AddNotifications(
            new Contract<CreateTodoCommand>()
                .IsGreaterThan(Title, 3, "Title", "Por favor, descreva melhor a tarefa!")
                .IsGreaterThan(User, 6, "User", "Usuário inválido!")
                .Requires()
        );
}
