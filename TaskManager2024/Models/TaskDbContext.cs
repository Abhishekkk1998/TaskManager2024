using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskManager2024.Models
{
    public class TaskDbContext : DbContext
    {

        public TaskDbContext() : base("TaskManagerConnection") { }
        public DbSet<Task> Tasks { get; set; }

    }
}