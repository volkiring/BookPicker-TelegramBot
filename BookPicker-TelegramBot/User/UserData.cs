using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot.User
{
    public class UserData
    {
        public int Id { get; set; }
        public Book CurrentBook { get; set; }

        public override string ToString()
        {
            return $"{Id}";
        }

    }
}
