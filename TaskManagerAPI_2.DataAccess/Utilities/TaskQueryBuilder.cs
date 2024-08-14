using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerAPI_2.Models;

namespace TaskManagerAPI_2.DataAccess.Utilities
{
    public static class TaskQueryBuilder
    {
        /// <summary>
        /// Applies search filters to the task we are looking for in the DB
        /// </summary>
        /// <param name="query"></param>
        /// <param name="IdBeginning"></param>
        /// <param name="IdEnd"></param>
        /// <param name="Title"></param>
        /// <param name="Description"></param>
        /// <param name="Status"></param>
        /// <param name="Priority"></param>
        /// <param name="DueDate"></param>
        /// <returns>IQueryable of TaskModel  </returns>

        public static IQueryable<TaskModel> ApplyFilters(
            IQueryable<TaskModel> query,
            int? IdBeginning,
            int? IdEnd,
            string? Title,
            string? Description,
            string? Status,
            string? Priority,
            DateOnly? DueDate

            )
        {
            if(IdBeginning != null)
            {
                query = query.Where(task => task.Id >= IdBeginning);
            }
            if (IdEnd != null)
            {
                query = query.Where(task => task.Id <= IdEnd);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                query = query.Where(task => task.Title.Contains(Title));
            }
            if (!string.IsNullOrEmpty(Description))
            {
                query = query.Where(task => task.Description.Contains(Description)); //TODO: add letter case support
            }
            if (!string.IsNullOrEmpty(Status))
            {
                query = query.Where(task => task.Status.Contains(Status));
            }
            if (!string.IsNullOrEmpty(Priority))
            {
                query = query.Where(task => task.Priority.Contains(Priority));
            }
            if(DueDate !=  null)
            {
                query = query.Where(task => task.DueDate.Equals(DueDate));
            }

            return query;

        }

        /// <summary>
        /// Applies paging to query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>IQueryable of task model</returns>
        public static IQueryable<TaskModel> ApplyPaging
            (
                IQueryable<TaskModel> query,
                int? pageNumber,
                int? pageSize
            )
        {
            if (pageNumber != null)
            {
                if(pageSize != null)
                {
                    query = query.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize);

                }
            }

            return query;
        }

    }
}
