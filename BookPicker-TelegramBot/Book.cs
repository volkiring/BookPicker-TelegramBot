using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int DateOfPublication { get; set; }
        public string LinkToRead { get; set; }

        public Book(string title, string author, int dateOfPublication, string linkToRead)
        {
            Title = title;
            Author = author;
            DateOfPublication = dateOfPublication;
            LinkToRead = linkToRead;
        }

        public override string ToString()
        {
            return $"{Author}, {Title}, {DateOfPublication}";
        }
    }
}
