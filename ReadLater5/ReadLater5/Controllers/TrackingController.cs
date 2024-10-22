using Data.Models;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    [Authorize]
    public class TrackingController : Controller
    {

        private readonly ITrackingService _trackingService;
        private readonly IBookmarkService _bookmarkService;

        public TrackingController(ITrackingService trackingService, IBookmarkService bookmarkService)
        {
            _trackingService = trackingService;
            _bookmarkService = bookmarkService;
        }

        // GET: ActivityTracking
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IEnumerable<ActivityTracking> activityTrackings = await _trackingService.GetAllTrackingsForUserAsync(userId);
            return View(activityTrackings);
        }

        [HttpPost]
        public async Task<IActionResult> TrackClick(int bookmarkId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string sourceUrl = Request.Headers["Referer"];

            var tracking = new ActivityTracking
            {
                UserId = userId,
                BookmarkId = bookmarkId,
                ClickedAt = DateTime.UtcNow,
                SourceUrl = sourceUrl
            };

            await _trackingService.AddActivityTrackingAsync(tracking);

            var bookmark = _bookmarkService.GetBookmark(bookmarkId, userId);
            if (bookmark == null)
            {
                return NotFound();
            }

            return Redirect(bookmark.URL);
        }

        public async Task<IActionResult> Summary()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<BookmarkStatistics> stats = await _trackingService.GetClickStatisticsAsync(userId);
            return View(stats);
        }


        [HttpPost]
        public async Task<IActionResult> GenerateShortUrl(int bookmarkId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookmark = _bookmarkService.GetBookmark(bookmarkId, userId);

            string shortUrl = Url.Action("Details", "Bookmarks", new { id = bookmarkId }, Request.Scheme);


            var tracking = new ActivityTracking
            {
                UserId = userId,
                BookmarkId = bookmarkId,
                ClickedAt = DateTime.UtcNow,
                SourceUrl = shortUrl
            };

            await _trackingService.AddActivityTrackingAsync(tracking);


            TempData["ShortUrl"] = shortUrl;
            return Redirect("Index");
        }

    }
}
