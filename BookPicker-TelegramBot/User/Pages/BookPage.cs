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
                case "Назад":
                    return new ChoosingBookPage().View(update, userState);
                case "Удалить из закладок":
                    userState.UserData.Bookmarks.Remove(userState.UserData.CurrentBook);
                    return new BookPage().View(update, userState);
                default:
                    userState.UserData.Bookmarks.Remove(userState.UserData.CurrentBook);
                    return new BookPage().View(update, userState);
            }
        }

        public PageResult View(Update update, UserState userState)
        {
            var currentBook = userState.UserData.CurrentBook;
            string text = currentBook.ToString();
            var IsExistBookmark = false;
            if (userState.UserData.Bookmarks!.Contains(currentBook))
            {
                IsExistBookmark = true;
            }
            var replyMarkup = GetReplyMarkup(currentBook, IsExistBookmark);

            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };

        }

        private IReplyMarkup GetReplyMarkup(Book book, bool IsExistBookmark)
        {
            if (IsExistBookmark)
            {
                return new InlineKeyboardMarkup(
              [
                  [
                        InlineKeyboardButton.WithUrl("Читать сейчас", book.LinkToRead)
                    ],

                    [
                       InlineKeyboardButton.WithCallbackData("Удалить из закладок")
                    ],

                    [
                        InlineKeyboardButton.WithCallbackData("Назад"),
                    ]
              ]);
            }

            else
            {
                return new InlineKeyboardMarkup(
[
  [
                        InlineKeyboardButton.WithUrl("Читать сейчас", book.LinkToRead)
                    ],

                    [
                       InlineKeyboardButton.WithCallbackData("Добавить в закладки")
                    ],

                    [
                        InlineKeyboardButton.WithCallbackData("Назад"),
                    ]
]);
            }
        }
    }
}

