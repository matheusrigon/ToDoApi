namespace Todo.Domain.Entities;

public class TodoItem : Entity
{
    public TodoItem(string title, string user,DateTime date)
    {
        Title = title;
        User = user;
        Done = false;
        Date = date;
    }

    public string Title {get;private set;}
    public string User {get;private set;}
    public  bool Done {get;private set;}
    public DateTime Date {get;private set;}

    public void MarkAsDone() 
        => Done = true;

    public void MarkAsUndone()
        => Done = false;
        
    public void UpdateTitle(string title)
        => Title = title;
    
}
