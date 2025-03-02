using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Todo.Domain.Api.Endpoints.Todo;
using Todo.Domain.Handlers;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Infra.Repositories;
using Todo.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{ Title = "TodoApi"} ));
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
                    opt.Authority = "https://securetoken.google.com/todos-17684";                    
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/todos-17684",
                        ValidateAudience = true,
                        ValidAudience = "todos-17684",
                        ValidateLifetime = true                    
                    };                    
                });

// builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddTransient<ITodoRepository, TodoRepository>();
builder.Services.AddTransient<TodoHandler, TodoHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();    
    
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { _ = endpoints.MapControllers(); });

app.MapGroup("v1/Todo")
    .MapProductsApi()
    .WithTags("Todo")
    .RequireAuthorization().WithOpenApi();

app.MapGet("/healthcheck", () => "Hello World!")
    .WithTags("HealthCheck")
    .WithOpenApi();

app.Run();