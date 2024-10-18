using Entity;
using Repositories;
using System;
using System.Collections.Generic;

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

        public Bookmark GetBookmark(int Id)
        {
            Bookmark bookmark = _bookmarkRepository.GetBookmark(Id);
            if (bookmark == null)
            {
                throw new KeyNotFoundException($"No bookmark found with Id = {Id}.");
            }
            return bookmark;
        }

        public List<Bookmark> GetBookmarks()
        {
            return _bookmarkRepository.GetBookmarks();
        }

        public void UpdateBookmark(Bookmark bookmark)
        {

            var existingBookmark = _bookmarkRepository.GetBookmark(bookmark.ID);
            if (existingBookmark == null)
            {
                throw new KeyNotFoundException($"No bookmark found with Id = {bookmark.ID}.");
            }

            _bookmarkRepository.UpdateBookmark(bookmark);
        }
    }
}
