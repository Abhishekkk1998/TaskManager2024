using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaskManager2024.Models;

namespace TaskManager2024.Services
{
    public class TaskService
    {
        private readonly TaskDbContext _context;

        public TaskService()
        {
            _context = new TaskDbContext();
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _context.Tasks.OrderBy(t => t.Priority).ToList();
        }

        public Task GetTaskById(int id)
        {
            return _context.Tasks.Find(id);
        }

        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public void MarkTaskAsCompleted(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                task.IsCompleted = true;
                _context.SaveChanges();
            }
        }
    }
}
