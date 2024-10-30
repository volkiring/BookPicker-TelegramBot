using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BookPicker_TelegramBot
{
    public class Reminder : IEquatable<Reminder>
    {
        public Book Book { get; set; }

        public TimeSpan Time { get; set; }

        public Reminder(Book book, TimeSpan time)
        {
            Book = book;
            Time = time;
        }

        public bool Equals(Reminder? other)
        {
            return other?.Book == Book && other.Time == Time;
        }
    }
}
