﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot.User
{
    public class UserData
    {
        public int Id { get; set; }
        public Filter CurrentFilter { get; set; }

        public Book CurrentBook { get; set; }

        public List<Book> Bookmarks = new List<Book>();

        public override string ToString()
        {
            return $"{Id}";
        }

    }
}
