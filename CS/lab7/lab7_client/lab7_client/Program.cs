using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Lab6_Client
{
    class Program
    {
        static HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            // Адрес сервера
            string baseUrl = "https://localhost:7227/api/users";

            Console.WriteLine("ПОЛУЧАЕМ СПИСОК ВСЕХ (GET)");
            var users = await httpClient.GetFromJsonAsync<List<Person>>(baseUrl);
            PrintUsers(users);

            Console.WriteLine("\nДОБАВЛЯЕМ НОВОГО (POST)");
            var newUser = new Person { Name = "Mike", Age = 31 };
            var postResponse = await httpClient.PostAsJsonAsync(baseUrl, newUser);
            var addedUser = await postResponse.Content.ReadFromJsonAsync<Person>();
            Console.WriteLine($"Добавлен: [{addedUser?.Id}] {addedUser?.Name}");

            Console.WriteLine("\n ИЗМЕНЯЕМ ДАННЫЕ (PUT");
            if (addedUser != null)
            {
                addedUser.Name = "Michael"; // Меняем имя Майка
                addedUser.Age = 32;         // Меняем возраст
                var putResponse = await httpClient.PutAsJsonAsync(baseUrl, addedUser);
                var updatedUser = await putResponse.Content.ReadFromJsonAsync<Person>();
                Console.WriteLine($"Успех! Изменен: [{updatedUser?.Id}] {updatedUser?.Name} ({updatedUser?.Age} лет)");
            }

            Console.WriteLine("\n УДАЛЯЕМ (DELETE)");
            var deleteResponse = await httpClient.DeleteAsync($"{baseUrl}/{addedUser?.Id}");
            if (deleteResponse.IsSuccessStatusCode)
            {
                var deletedUser = await deleteResponse.Content.ReadFromJsonAsync<Person>();
                Console.WriteLine($"Удален: {deletedUser?.Name}");
            }

            Console.WriteLine("\n ИТОГОВЫЙ СПИСОК (GET)");
            users = await httpClient.GetFromJsonAsync<List<Person>>(baseUrl);
            PrintUsers(users);

            Console.ReadLine();
        }

        //чтобы не писать цикл вывода сто раз
        static void PrintUsers(List<Person>? users)
        {
            if (users != null)
            {
                foreach (var u in users)
                {
                    Console.WriteLine($"[{u.Id}] {u.Name} - {u.Age} лет");
                }
            }
        }
    }

    // Класс человека
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}