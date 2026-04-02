using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//дл€ генерации айди
int id = 1;

//начальный список
List<Person> users = new List<Person>
{
    new Person { Id = id++, Name = "Tom", Age = 37 },
    new Person { Id = id++, Name = "Bob", Age = 41 },
    new Person { Id = id++, Name = "Sam", Age = 24 }
};

//весь список пользователей
app.MapGet("/api/users", () => users);

//один пользователь по ID
app.MapGet("/api/users/{id}", (int id) =>
{
    Person? user = users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
        return Results.NotFound(new { message = "ѕользователь не найден" });
    }
    return Results.Json(user);
});

// нвоый пользователь в список
app.MapPost("/api/users", (Person user) =>
{
    user.Id = id++; // ѕрисваиваем новый ID
    users.Add(user); // ƒобавл€ем в список
    return user;
});

// изменить данные существующего пользовател€
app.MapPut("/api/users", (Person userData) =>
{
    var user = users.FirstOrDefault(u => u.Id == userData.Id);
    if (user == null)
    {
        return Results.NotFound(new { message = "ѕользователь не найден" });
    }

    // ќбновл€ем данные
    user.Age = userData.Age;
    user.Name = userData.Name;

    return Results.Json(user);
});

// 5.удалить пользовател€
app.MapDelete("/api/users/{id}", (int id) =>
{
    Person? user = users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
        return Results.NotFound(new { message = "ѕользователь не найден" });
    }

    users.Remove(user); // ”дал€ем из списка
    return Results.Json(user);
});

app.Run();

// описание класса Person
class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}