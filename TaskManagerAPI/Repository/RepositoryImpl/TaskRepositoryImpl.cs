using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;
using TaskManagerAPI.Repository;
using TaskManagerAPI.Repository.IRepository;

namespace TaskManagerAPI.Repository.RepositoryImpl
{
    public class TaskRepositoryImpl : RepositoryImpl<TaskModel>, ITaskRepository
    {
        public TaskRepositoryImpl(DbContext context) : base(context)
        {
        }





    }
}
