using BookPicker_TelegramBot.Storage;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class ChoosingBookPage : IPage
    {
        public PageResult Handle(Update update, UserState Userstate)
        {
            throw new NotImplementedException();
        }

        public PageResult View(Update update, UserState userState)
        {
            if (userState.UserData.CurrentStatus.Item1 == "genre")
            {
                var genre = userState.UserData.CurrentStatus.Item2;
                var text = @"Вот книги по выбранному вами жанру";
                var replyMarkup = GetReplyMarkup(UserBooksStorage.Books.Where(x => x.Genre == genre));

                return new PageResult(text, replyMarkup)
                {
                    UpdatedUserState = new UserState(this, userState.UserData)
                };
            }

            else
            {
                var author = userState.UserData.CurrentStatus.Item2;
                var text = @"Вот книги выбранного вами автора";
                var replyMarkup = GetReplyMarkup(UserBooksStorage.Books.Where(x => x.Author == author));

                return new PageResult(text, replyMarkup)
                {
                    UpdatedUserState = new UserState(this, userState.UserData)
                };
            }
        }

        private IReplyMarkup GetReplyMarkup(IEnumerable<Book> books)
        {
            return Book.CreateInlineKeyboardBooks(books.Select(x => x.ToString()));
        }

    }
}