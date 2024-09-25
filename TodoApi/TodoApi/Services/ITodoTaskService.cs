using TodoApi.Models;
using System.Collections.Generic;

namespace TodoApi.Services
{
    public interface ITodoTaskService
    {
        TodoTask CreateTask(TodoTask task);
        TodoTask GetTaskById(int id);
        IEnumerable<TodoTask> GetAllTasks();
        TodoTask UpdateTask(TodoTask task);
        void DeleteTask(int id);
    }
}



