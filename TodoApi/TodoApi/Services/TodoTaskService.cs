using TodoApi.Data;
using TodoApi.Models;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Services;

public class TodoTaskService : ITodoTaskService
{
    private readonly TodoDbContext _context;

    public TodoTaskService(TodoDbContext context)
    {
        _context = context;
    }

    public TodoTask CreateTask(TodoTask task)
    {
        ValidateTask(task);
        _context.TodoTasks.Add(task);
        _context.SaveChanges();
        return task;
    }

    public TodoTask GetTaskById(int id)
    {
        return _context.TodoTasks.Find(id);
    }

    public IEnumerable<TodoTask> GetAllTasks()
    {
        return _context.TodoTasks.ToList();
    }

    public TodoTask UpdateTask(TodoTask task)
    {
        ValidateTask(task);
        _context.TodoTasks.Update(task);
        _context.SaveChanges();
        return task;
    }

    public void DeleteTask(int id)
    {
        var task = GetTaskById(id);
        if (task != null)
        {
            _context.TodoTasks.Remove(task);
            _context.SaveChanges();
        }
    }

    private void ValidateTask(TodoTask task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
            throw new ArgumentException("Task title cannot be empty.");
        if (task.Description.Length < 10)
            throw new ArgumentException("Task description must be at least 10 characters long.");
    }
}

