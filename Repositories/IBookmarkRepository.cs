using Entity;
using System.Collections.Generic;

namespace Repositories
{
    public interface IBookmarkRepository
    {
        Bookmark CreateBookmark(Bookmark bookmark);
        List<Bookmark> GetBookmarks(string userId);
        Bookmark GetBookmark(int id, string userId);
        void UpdateBookmark(Bookmark bookmark);
        void DeleteBookmark(Bookmark bookmark);
    }
}
