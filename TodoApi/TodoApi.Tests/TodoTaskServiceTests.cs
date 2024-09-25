using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using TodoApi.Models;
using TodoApi.Services;
using TodoApi.Data;

namespace TodoApi.Tests
{
    public class TodoTaskServiceTests
    {
        private readonly ITodoTaskService _service;

        public TodoTaskServiceTests()
        {
            // Arrange
            var data = new List<TodoTask>
            {
                new TodoTask
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Description for Task 1",
                    IsCompleted = false,
                    DueDate = DateTime.Now.AddDays(1)
                },
                new TodoTask
                {
                    Id = 2,
                    Title = "Task 2",
                    Description = "Description for Task 2",
                    IsCompleted = true,
                    DueDate = DateTime.Now.AddDays(2)
                }
            }.AsQueryable();

            var mockContext = MockExtensions.CreateMockDbContext(data);

            _service = new TodoTaskService(mockContext.Object);
        }

        [Fact]
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            // Act
            var result = _service.GetAllTasks().ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Task 1", result[0].Title);
            Assert.Equal("Task 2", result[1].Title);
        }

        // Additional tests can go here...
    }
}
