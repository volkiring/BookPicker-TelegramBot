using BookPicker_TelegramBot.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot.Firebase
{
    public class UserStateFirebase
    {
        public UserData UserData { get; set; }
        public List<string> PageNames { get; set; }
    }
}
