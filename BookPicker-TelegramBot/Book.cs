using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot
{
    public class Book : IEquatable<Book>    
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public string Genre { get; set; }
        public int DateOfPublication { get; set; }
        public string LinkToRead { get; set; }

        public Book(string title, string author, string genre, int dateOfPublication, string linkToRead)
        {
            Title = title;
            Author = author;
            Genre = genre;
            DateOfPublication = dateOfPublication;
            LinkToRead = linkToRead;
        }

        public override string ToString()
        {
            return $"{Author}, {Title}";
        }

        public bool Equals(Book? other)
        {
            return Title == other?.Title && Author == other.Author && DateOfPublication == other.DateOfPublication && Genre == other.Genre;
        }


        public static InlineKeyboardMarkup CreateInlineKeyboardBooks(IEnumerable<string> buttons)
        { 
            var rows = new List<InlineKeyboardButton[]>();

            foreach (var title in buttons)
            {
                var button = InlineKeyboardButton.WithCallbackData(title);
                rows.Add([button]);
            }

            var backbutton = InlineKeyboardButton.WithCallbackData("Назад");
            rows.Add([backbutton]);
            return new InlineKeyboardMarkup(rows);
        }
    }
}
