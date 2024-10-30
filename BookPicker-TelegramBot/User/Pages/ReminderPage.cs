using BookPicker_TelegramBot.Storage;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class ReminderPage : IPage
    {
        public PageResult Handle(Update update, UserState userState)
        {
            switch (update.CallbackQuery.Data)
            {
                case "Назад":
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                default:
                    var choosedBook = update!.CallbackQuery!.Data;
                    userState.UserData.CurrentBook = BooksStorage.Books.FirstOrDefault(x => x.Title == choosedBook);
                    return new BookPage().View(update, userState);
            }
        }

        public PageResult View(Update update, UserState userState)
        {
            var text = @"Вот книги, которые Вы добавили для ежедневного чтения:";
            var replyMarkup = GetReplyMarkup(userState);

            if (!userState.Pages.Contains(this))
            {
                userState.AddPage(this);
            }

            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        public IReplyMarkup GetReplyMarkup(UserState userState)
        {
            return Book.CreateInlineKeyboardBooks(userState.UserData.Reminders.Select(x => $"{x.Book.ToString()} " + string.Format("{0:00}:{1:00}", x.Time.Hours, x.Time.Minutes)));
        }
    }
}