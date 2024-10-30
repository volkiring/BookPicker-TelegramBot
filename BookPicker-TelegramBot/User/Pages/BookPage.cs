using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class BookPage : IPage
    {
        public PageResult Handle(Update update, UserState userState)
        {
            switch (update.CallbackQuery.Data)
            {
                case "Добавить напоминание":
                    return new ChooseReminderTimePage().View(update, userState);
                case "Удалить из закладок":
                    userState.UserData.Bookmarks.Remove(userState.UserData.CurrentBook);
                    return new BookPage().View(update, userState);
                case "Удалить напоминание":
                    userState.UserData.Reminders.RemoveAll(x => x.Book.Equals(userState.UserData.CurrentBook));
                    return new BookPage().View(update, userState);
                case "Добавить в закладки":
                    userState.UserData.Bookmarks.Add(userState.UserData.CurrentBook);
                    return new BookPage().View(update, userState);
                default:
                    while (userState.CurrentPage.GetType() != typeof(StartPage))
                    {
                        userState.Pages.Pop();
                    }
                    return userState.CurrentPage.View(update, userState);
            }
        }

        public PageResult View(Update update, UserState userState)
        {
            var currentBook = userState.UserData.CurrentBook;
            string text = currentBook.ToString();
            var IsExistBookmark = false;
            var reminderBooks = userState.UserData?.Reminders.Select(x => x.Book);
            var IsExistReminder = false;

            if (userState.UserData.Bookmarks!.Contains(currentBook))
            {
                IsExistBookmark = true;
            }

            if (reminderBooks!.Contains(currentBook)) 
            {
                IsExistReminder = true;
            }


            var replyMarkup = GetReplyMarkup(currentBook, IsExistBookmark, IsExistReminder);

            if (!userState.Pages.Contains(this))
            {
                userState.AddPage(this);
            }
            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }


        private IReplyMarkup GetReplyMarkup(Book book, bool IsExistBookmark, bool IsExistReminder)
        {
            var buttons = new List<List<InlineKeyboardButton>>
    {
        new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithUrl("Читать сейчас", book.LinkToRead)
        }
    };

            if (IsExistBookmark)
            {
                buttons.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("Удалить из закладок")
        });
            }
            else
            {
                buttons.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("Добавить в закладки")
        });
            }

            
            if (IsExistReminder)
            {
                buttons.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("Удалить напоминание")
        });
            }
            else
            {
                buttons.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("Добавить напоминание")
        });
            }

            
            buttons.Add(new List<InlineKeyboardButton>
    {
        InlineKeyboardButton.WithCallbackData("Вернуться в главное меню")
    });

            return new InlineKeyboardMarkup(buttons);
        }

    }
}


