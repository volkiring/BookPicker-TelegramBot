using BookPicker_TelegramBot.Storage;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class ChoosingBookPage : IPage
    {
        public PageResult Handle(Update update, UserState userstate)
        {
            switch (update.CallbackQuery.Data)
            {
                case "Назад":
                    userstate.Pages.Pop();
                    return userstate.CurrentPage.View(update, userstate);
                default:
                    var choosedBook = update!.CallbackQuery!.Data;
                    userstate.UserData.CurrentBook = UserBooksStorage.Books.FirstOrDefault(x => x.Title == choosedBook);
                    return new BookPage().View(update, userstate);
            }
        }

        public PageResult View(Update update, UserState userState)
        {
            if (userState.UserData.CurrentFilter.Type == FilterType.Genre)
            {
                var genre = userState.UserData.CurrentFilter.Value;
                var text = @"Вот книги по выбранному вами жанру";
                var replyMarkup = GetReplyMarkup(UserBooksStorage.Books.Where(x => x.Genre == genre));

                userState.AddPage(this);
                return new PageResult(text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }

            else
            {
                var author = userState.UserData.CurrentFilter.Value;
                var text = @"Вот книги выбранного вами автора";
                var replyMarkup = GetReplyMarkup(UserBooksStorage.Books.Where(x => x.Author == author));


                userState.Pages.Push(this);
                return new PageResult(text, replyMarkup)
                {
                    UpdatedUserState = userState
                };
            }
        }

        private IReplyMarkup GetReplyMarkup(IEnumerable<Book> books)
        {
            return Book.CreateInlineKeyboardBooks(books.Select(x => x.ToString()));
        }

    }
}