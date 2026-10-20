using System.Collections.Generic;

namespace ToDoList
{
    public class TodoService
    {
        private readonly ITaskRepository _repository;
        private List<TodoModel> _tasks;

        public TodoService(ITaskRepository repository)
        {
            _repository = repository;
            _tasks = new List<TodoModel>();
        }

        public void LoadTasks()
        {
            _tasks = _repository.LoadTasks();
        }

        public void SaveTasks()
        {
            _repository.SaveTasks(_tasks);
        }

        public List<TodoModel> GetAllTasks()
        {
            return _tasks;
        }

        public void AddTask(TodoModel task)
        {
            _tasks.Add(task);
        }

        public void DeleteTask(TodoModel task)
        {
            _tasks.Remove(task);
        }

        public void ChangeStatus(TodoModel task, TaskStatus newStatus)
        {
            task.Status = newStatus;
        }
    }
}