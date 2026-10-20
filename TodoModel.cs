using System;
using System.Text.Json.Serialization;

namespace ToDoList
{
    public enum TaskPriority
    {
        Низкий,
        Средний,
        Высокий
    }

    public enum TaskStatus
    {
        НеНачата,
        ВПроцессе,
        Выполнена
    }

    public class TodoModel
    {
        private string _title;
        private string _description;

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название не может быть пустым.");
                _title = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value ?? string.Empty;
            }
        }

        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }

        public TodoModel()
        {
            CreationDate = DateTime.Now;
            DueDate = DateTime.Now.AddDays(1);
            Priority = TaskPriority.Низкий;
            Status = TaskStatus.НеНачата;
        }
    }
}