using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        // Mark the DbSet as virtual so it can be mocked
        public virtual DbSet<TodoTask> TodoTasks { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
