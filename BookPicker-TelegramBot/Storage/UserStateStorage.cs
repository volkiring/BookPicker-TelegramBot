using BookPicker_TelegramBot.User;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot.Storage
{
    public class UserStateStorage
    {
        private readonly ConcurrentDictionary<long,UserState> cache = new ConcurrentDictionary<long,UserState>();

        public void AddOrUpdate(long telegramId, UserState userState)
        {
            cache.AddOrUpdate(telegramId, userState, (x, y) => userState);
        }

        public bool TryGet(long telegramId, out UserState userState)
        {
            return cache.TryGetValue(telegramId, out userState);
        }
    }
}
