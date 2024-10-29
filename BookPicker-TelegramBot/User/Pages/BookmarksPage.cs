using BookPicker_TelegramBot.Storage;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class BookmarksPage : IPage
    {
        public PageResult Handle(Update update, UserState userState)
        {
            switch (update.CallbackQuery.Data)
            {
                case "Назад":
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                default:
                    var choosedBook = update.CallbackQuery.Data;
                    userState.UserData.CurrentBook = UserBooksStorage.Books.FirstOrDefault(x => x.Title == choosedBook);
                    return new BookPage().View(update, userState);

            }
        }

        public PageResult View(Update update, UserState userState)
        {
            var text = @"Вот книги, которые Вы добавили в закладки:";
            var replyMarkup = GetReplyMarkup(userState);

            userState.AddPage(this);
            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        private IReplyMarkup GetReplyMarkup(UserState userState)
        {
            return Book.CreateInlineKeyboardBooks(userState.UserData.Bookmarks.Select(x => x.ToString()));
        }
    }
}