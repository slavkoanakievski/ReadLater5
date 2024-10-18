using Data;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadLater5.Controllers
{
    public class BookmarksController : Controller


    {
        private readonly ICategoryService _categoryService;
        private readonly IBookmarkService _bookmarkService;

        public BookmarksController(ICategoryService categoryService, IBookmarkService bookmarkService)
        {
            _categoryService = categoryService;
            _bookmarkService = bookmarkService;
        }
        public IActionResult Index()
        {
            List<Bookmark> model = _bookmarkService.GetBookmarks();
            return View(model);
        }

        public ActionResult Create()
        {
            var categories = _categoryService.GetCategories();
            ViewBag.CategoryList = new SelectList(categories, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bookmark bookmark, string newCategoryName)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(newCategoryName))
                {
                    var existingCategory = _categoryService.GetCategory(newCategoryName);

                    if (existingCategory != null)
                    {
                        bookmark.CategoryId = existingCategory.ID;
                    }
                    else
                    {
                        var newCategory = new Category { Name = newCategoryName };
                        _categoryService.CreateCategory(newCategory);
                        bookmark.CategoryId = newCategory.ID;
                    }
                }

                bookmark.CreateDate = DateTime.Now;
                _bookmarkService.CreateBookmark(bookmark);

                return RedirectToAction("Index");
            }

            var categories = _categoryService.GetCategories();
            ViewBag.CategoryList = new SelectList(categories, "ID", "Name", bookmark.CategoryId);

            return View(bookmark);
        }
    }
}
