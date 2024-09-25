using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Tests
{
    public static class MockExtensions
    {
        public static Mock<TodoDbContext> CreateMockDbContext(IQueryable<TodoTask> data)
        {
            var mockSet = new Mock<DbSet<TodoTask>>();

            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TodoTask>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TodoDbContext>(new DbContextOptions<TodoDbContext>());

            // Set up the TodoTasks DbSet to return the mock DbSet
            mockContext.Setup(c => c.TodoTasks).Returns(mockSet.Object);

            return mockContext;
        }
    }
}
