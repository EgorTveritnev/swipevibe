using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    public class StreamController : Controller
    {
        // GET: Stream
        public ActionResult Index()
        {
            return View();
        }

        // GET: Stream/Live/5
        public ActionResult Live(int id)
        {
            ViewBag.StreamId = id;
            return View();
        }

        // GET: Stream/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stream/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // Логика создания нового стрима
                // В реальном приложении здесь будет обращение к сервису стриминга
                
                return RedirectToAction("Live", new { id = 1 }); // Перенаправляем на созданный стрим
            }
            catch
            {
                return View();
            }
        }

        // GET: Stream/Categories
        public ActionResult Categories()
        {
            return View();
        }

        // GET: Stream/Category/5
        public ActionResult Category(string id)
        {
            ViewBag.CategoryName = id;
            return View();
        }
    }
} 