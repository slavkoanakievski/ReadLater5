using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System;
using System.Collections.Generic;

namespace ReadLater5.Controllers
{
    public class BookmarksController : Controller


    {
        private readonly ICategoryService _categoryService;
        private readonly IBookmarkService _bookmarkService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookmarksController(
             ICategoryService categoryService
            , IBookmarkService bookmarkService
            , UserManager<ApplicationUser> userManager)
        {
            _categoryService = categoryService;
            _bookmarkService = bookmarkService;
            _userManager = userManager;
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User);
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
            string userId = GetUserId();
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id, userId);
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
                string userId = GetUserId();
                var existingBookmark = _bookmarkService.GetBookmark(bookmark.ID, userId);

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
            string userId = GetUserId();
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id, userId);

            return View(bookmark);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            string userId = GetUserId();
            Bookmark bookmark = _bookmarkService.GetBookmark(id, userId);
            _bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id)
        {
            string userId = GetUserId();
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id, userId);
            return View(bookmark);
        }


    }
}
