using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot.Storage
{
    public static class UserBooksStorage
    {
        public static ConcurrentDictionary<long, List<Book>> Bookmarks = new();
        
    }
}
