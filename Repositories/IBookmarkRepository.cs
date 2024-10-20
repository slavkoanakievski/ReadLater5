﻿using Entity;
using System.Collections.Generic;

namespace Repositories
{
    public interface IBookmarkRepository
    {
        Bookmark CreateBookmark(Bookmark bookmark);
        List<Bookmark> GetBookmarks();
        Bookmark GetBookmark(int Id);
        void UpdateBookmark(Bookmark bookmark);
        void DeleteBookmark(Bookmark bookmark);
    }
}
