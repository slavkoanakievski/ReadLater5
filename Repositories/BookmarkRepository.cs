using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private ReadLaterDataContext _readLaterDataContext;

        public BookmarkRepository(ReadLaterDataContext readLaterDataContext)
        {
            _readLaterDataContext = readLaterDataContext;
        }
        public Bookmark GetBookmark(int id, string userId)
        {
            return _readLaterDataContext.Bookmark
                .Where(b => b.ID == id && b.UserId == userId)
                .Include(b => b.Category)
                .FirstOrDefault();
        }

        public List<Bookmark> GetBookmarks()
        {
            return _readLaterDataContext.Bookmark
                .Include(b => b.Category)
                .ToList();
        }

        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            _readLaterDataContext.Bookmark.Add(bookmark);
            _readLaterDataContext.SaveChanges();
            return bookmark;
        }
        public void UpdateBookmark(Bookmark bookmark)
        {
            _readLaterDataContext.Update(bookmark);
            _readLaterDataContext.SaveChanges();
        }
        public void DeleteBookmark(Bookmark bookmark)
        {
            _readLaterDataContext.Remove(bookmark);
            _readLaterDataContext.SaveChanges();
        }
    }
}
