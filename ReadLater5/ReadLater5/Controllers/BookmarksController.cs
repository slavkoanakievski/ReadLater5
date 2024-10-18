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

        public IActionResult Edit(int? id)
        {
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            var categories = _categoryService.GetCategories();
            ViewBag.CategoryList = new SelectList(categories, "ID", "Name", bookmark.CategoryId);

            return View(bookmark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bookmark bookmark, string newCategoryName)
        {
            if (ModelState.IsValid)
            {
                var existingBookmark = _bookmarkService.GetBookmark(bookmark.ID);

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

                existingBookmark.URL = bookmark.URL;
                existingBookmark.ShortDescription = bookmark.ShortDescription;
                existingBookmark.CategoryId = bookmark.CategoryId;

                _bookmarkService.UpdateBookmark(existingBookmark);

                return RedirectToAction("Index");
            }

            var categories = _categoryService.GetCategories();
            ViewBag.CategoryList = new SelectList(categories, "ID", "Name", bookmark.CategoryId);

            return View(bookmark);
        }

        public IActionResult Delete(int? id)
        {
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);

            return View(bookmark);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Bookmark bookmark = _bookmarkService.GetBookmark(id);
            _bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id)
        {

            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            return View(bookmark);
        }


    }
}
