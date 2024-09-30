using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookPicker_TelegramBot.User.Pages;

namespace BookPicker_TelegramBot.User
{
    public record class UserState(IPage Page, UserData UserData);
}
