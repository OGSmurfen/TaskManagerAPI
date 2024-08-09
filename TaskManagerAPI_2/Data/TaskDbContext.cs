using Microsoft.EntityFrameworkCore;
using TaskManagerAPI_2.Models;






namespace TaskManagerAPI_2.Data
{
    public class TaskDbContext : DbContext
    {
        public DbSet<TaskModel> Tasks { get; set; }



        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
            
        }

        


    }
}
