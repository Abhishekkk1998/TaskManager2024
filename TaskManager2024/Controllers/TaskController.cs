using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager2024.Models;
using TaskManager2024.Services;

namespace TaskManager2024.Controllers
{
    public class TaskController : Controller
    {
        private TaskDbContext db = new TaskDbContext();
        private readonly TaskService _taskService;
        public TaskController()
        {
            _taskService = new TaskService();
        }
        public ActionResult Index()
        {
            var tasks = _taskService.GetAllTasks();
            return View(tasks);
           // return View(db.Tasks.ToList());
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Task task = db.Tasks.Find(id);
            //if (task == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(task);
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskId,Title,Description,IsCompleted,Priority")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskId,Title,Description,IsCompleted,Priority")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Task task = db.Tasks.Find(id);
            //if (task == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(task);
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Task task = db.Tasks.Find(id);
            //db.Tasks.Remove(task);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            _taskService.DeleteTask(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult MarkCompleted(int id)
        {
            _taskService.MarkTaskAsCompleted(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult UpdateTaskPriorities(string order)
        {
            try
            {
                var taskIds = order.Split(',').Select(int.Parse).ToList();

                for (int i = 0; i < taskIds.Count; i++)
                {
                    var task = _taskService.GetTaskById(taskIds[i]);
                    if (task != null)
                    {
                        task.Priority = i + 1;
                        _taskService.UpdateTask(task);
                    }
                }

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
    }
}
