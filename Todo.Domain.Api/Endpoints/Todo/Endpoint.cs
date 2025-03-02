using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Endpoints.Todo;

public static class Endpoint
{
    [Authorize]
    public static RouteGroupBuilder MapProductsApi(this RouteGroupBuilder group)
    {
        group.MapPost("/Create", ([FromBody]CreateTodoCommand command, [FromServices]TodoHandler handler, ClaimsPrincipal user) =>
            (GenericCommandResult)handler.Handle(SetUserFromClaims(command, user)));
        group.MapPut("/Update", ([FromBody]UpdateTodoCommand command, [FromServices]TodoHandler handler, ClaimsPrincipal user) =>
            (GenericCommandResult)handler.Handle(SetUserFromClaims(command, user)));
        group.MapPut("/MarkAsDone", ([FromBody]MarkTodoAsDoneCommand command, [FromServices]TodoHandler handler, ClaimsPrincipal user) =>
            (GenericCommandResult)handler.Handle(SetUserFromClaims(command, user)));
        group.MapPut("/MarkAsUndone", ([FromBody]MarkTodoAsUndoneCommand command, [FromServices]TodoHandler handler, ClaimsPrincipal user) =>
            (GenericCommandResult)handler.Handle(SetUserFromClaims(command, user)));
        group.MapGet("", ([FromServices]ITodoRepository repository, ClaimsPrincipal user) =>
            repository.GetAll(GetUserFromClaims(user)));
        group.MapGet("/Done", ([FromServices]ITodoRepository repository, ClaimsPrincipal user) =>
            repository.GetAllDone(GetUserFromClaims(user)));
        group.MapGet("/Undone", ([FromServices]ITodoRepository repository, ClaimsPrincipal user) =>
            repository.GetAllUndone(GetUserFromClaims(user)));
        group.MapGet("/Done/Today", ([FromServices]ITodoRepository repository, ClaimsPrincipal user) =>
            repository.GetAllByPeriod(GetUserFromClaims(user), DateTime.Now.Date, true));
        group.MapGet("/Done/Tomorrow", ([FromServices]ITodoRepository repository, ClaimsPrincipal user) =>
            repository.GetAllByPeriod(GetUserFromClaims(user), DateTime.Now.AddDays(1).Date, true));
        group.MapGet("/Undone/Today", ([FromServices]ITodoRepository repository, ClaimsPrincipal user) =>
            repository.GetAllByPeriod(GetUserFromClaims(user), DateTime.Now.Date, false));
        group.MapGet("/Undone/Tomorrow", ([FromServices]ITodoRepository repository, ClaimsPrincipal user) =>
            repository.GetAllByPeriod(GetUserFromClaims(user), DateTime.Now.AddDays(1).Date, false));

        return group;
    }

    public static CreateTodoCommand SetUserFromClaims(CreateTodoCommand command, ClaimsPrincipal user){
        command.User = GetUserFromClaims(user);

        return command;
    }

    public static UpdateTodoCommand SetUserFromClaims(UpdateTodoCommand command, ClaimsPrincipal user){
        command.User = GetUserFromClaims(user);

        return command;
    }

    public static MarkTodoAsDoneCommand SetUserFromClaims(MarkTodoAsDoneCommand command, ClaimsPrincipal user){
        command.User = GetUserFromClaims(user);

        return command;
    }

    public static MarkTodoAsUndoneCommand SetUserFromClaims(MarkTodoAsUndoneCommand command, ClaimsPrincipal user){
        command.User = GetUserFromClaims(user);

        return command;
    }

    public static string GetUserFromClaims(ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value ?? "";
    
}
