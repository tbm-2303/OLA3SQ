using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; // Ensure you have this using directive for DbSet
using Xunit;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Tests
{
    public class TodoTaskServiceCreateTaskTests
    {
        private readonly ITodoTaskService _service;
        private readonly Mock<TodoDbContext> _mockContext;
        private readonly Mock<DbSet<TodoTask>> _mockSet;

        public TodoTaskServiceCreateTaskTests()
        {
            // Arrange: Create a mock DbSet for TodoTask
            _mockSet = new Mock<DbSet<TodoTask>>();

            // Create DbContextOptions for the mock TodoDbContext
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use an in-memory database for testing
                .Options;

            // Create the TodoDbContext with the options
            _mockContext = new Mock<TodoDbContext>(options);

            // Set up TodoDbContext to return the mocked DbSet
            _mockContext.Setup(m => m.TodoTasks).Returns(_mockSet.Object);

            _service = new TodoTaskService(_mockContext.Object);
        }

        [Fact]
        public void CreateTask_ShouldAddNewTask()
        {
            // Arrange
            var newTask = new TodoTask
            {
                Title = "New Task",
                Description = "This is a task description.",
                IsCompleted = false,
                DueDate = DateTime.Now.AddDays(7) // Set a due date for the task
            };

            // Act
            var result = _service.CreateTask(newTask);

            // Assert
            _mockSet.Verify(m => m.Add(It.IsAny<TodoTask>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once); // Ensure SaveChanges was called
            Assert.Equal(newTask.Title, result.Title);
            Assert.Equal(newTask.Description, result.Description);
            Assert.Equal(newTask.IsCompleted, result.IsCompleted);
            Assert.Equal(newTask.DueDate, result.DueDate);
        }
    }
}
