using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwipeVibe.Web.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            // Логика получения всех категорий
            // ...
            
            return View();
        }

        // GET: Category/Videos/categoryName
        public ActionResult Videos(string id)
        {
            ViewBag.CategoryName = id;
            
            // Логика получения видео из указанной категории
            // ...
            
            return View();
        }

        // GET: Category/Trending/categoryName
        public ActionResult Trending(string id)
        {
            ViewBag.CategoryName = id;
            
            // Логика получения трендовых видео из указанной категории
            // ...
            
            return View();
        }

        // GET: Category/New/categoryName
        public ActionResult New(string id)
        {
            ViewBag.CategoryName = id;
            
            // Логика получения новых видео из указанной категории
            // ...
            
            return View();
        }

        // GET: Category/Popular/categoryName
        public ActionResult Popular(string id)
        {
            ViewBag.CategoryName = id;
            
            // Логика получения популярных видео из указанной категории
            // ...
            
            return View();
        }
    }
} 