using Entity;
using Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {

        private IBookmarkRepository _bookmarkRepository;
        public BookmarkService(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }
        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            return _bookmarkRepository.CreateBookmark(bookmark);
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            if (bookmark == null)
            {
                throw new ArgumentNullException(nameof(bookmark), "Bookmark cannot be null!");
            }

            _bookmarkRepository.DeleteBookmark(bookmark);
        }

        public Bookmark GetBookmark(int id, string userId)
        {
            Bookmark bookmark = _bookmarkRepository.GetBookmark(id, userId);
            if (bookmark == null)
            {
                throw new KeyNotFoundException($"No bookmark found with Id = {id}.");
            }
            return bookmark;
        }

        public List<Bookmark> GetBookmarks()
        {
            return _bookmarkRepository.GetBookmarks();
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            var existingBookmark = _bookmarkRepository.GetBookmark(bookmark.ID, bookmark.UserId);
            if (existingBookmark == null)
            {
                throw new KeyNotFoundException($"No bookmark found with Id = {bookmark.ID}.");
            }

            _bookmarkRepository.UpdateBookmark(bookmark);
        }
    }
}
