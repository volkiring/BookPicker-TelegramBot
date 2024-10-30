using BookPicker_TelegramBot.Storage;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class AuthorsPage : IPage
    {
        public PageResult Handle(Update update, UserState userState)
        {
            switch (update!.CallbackQuery!.Data)
            {
                case "Назад":
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                default:
                    userState.UserData.CurrentFilter = new Filter(FilterType.Author, update.CallbackQuery.Data);
                    return new ChoosingBookPage().View(update, userState);
            }
        }

        public PageResult View(Update update, UserState userState)
        {
            var text = @"Вот список авторов, книги которых вы можете выбрать";
            var replyMarkup = GetReplyMarkup();

            if (!userState.Pages.Contains(this))
            {
                userState.AddPage(this);
            }
            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        private IReplyMarkup GetReplyMarkup()
        {
            return Book.CreateInlineKeyboardBooks(BooksStorage.Books.Select(x => x.Author));
        }


    }
}