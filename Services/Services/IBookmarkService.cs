using Entity;
using System.Collections.Generic;

namespace Services
{
    public interface IBookmarkService
    {
        Bookmark CreateBookmark(Bookmark bookmark, string userId);
        List<Bookmark> GetBookmarks(string userId);
        Bookmark GetBookmark(int id, string userId);
        void UpdateBookmark(Bookmark bookmark);
        void DeleteBookmark(Bookmark bookmark);
    }
}
