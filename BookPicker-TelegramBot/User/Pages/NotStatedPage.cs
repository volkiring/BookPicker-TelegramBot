using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BookPicker_TelegramBot.User.Pages
{
    public class NotStatedPage : IPage
    {
        public PageResult Handle(Update update, UserState userState)
        {
            return new StartPage().View(update, userState);
        }

        public PageResult View(Update update, UserState userState)
        {
            return null;
        }
    }
}
