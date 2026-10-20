using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ToDoList
{
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath;

        public JsonTaskRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<TodoModel> LoadTasks()
        {
            if (!File.Exists(_filePath))
                return new List<TodoModel>();

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<TodoModel>>(json) ?? new List<TodoModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
                return new List<TodoModel>();
            }
        }

        public void SaveTasks(List<TodoModel> tasks)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(tasks, options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
        }
    }
}