using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot.Storage
{
    public static class BooksStorage
    {
        public static List<Book> Books = new List<Book>
        {
            new Book("Война и мир", "Лев Толстой", "Роман", 1869, "https://example.com/war_and_peace"),
            new Book("Преступление и наказание", "Фёдор Достоевский", "Роман", 1866, "https://example.com/crime_and_punishment"),
            new Book("Мастер и Маргарита", "Михаил Булгаков", "Фантастический роман", 1967, "https://example.com/master_and_margarita"),
            new Book("Анна Каренина", "Лев Толстой", "Роман", 1877, "https://example.com/anna_karenina"),
            new Book("Евгений Онегин", "Александр Пушкин", "Роман в стихах", 1833, "https://example.com/eugene_onegin")
        };
    }
}
