using System.Collections.Generic;

namespace ToDoList
{
    public interface ITaskRepository
    {
        List<TodoModel> LoadTasks();

        void SaveTasks(List<TodoModel> tasks);
    }
}