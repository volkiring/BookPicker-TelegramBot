using BookPicker_TelegramBot.Storage;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace BookPicker_TelegramBot.User.Pages
{
    public class GenrePage : IPage
    {
        public PageResult Handle(Update update, UserState userState)
        {
            switch (update!.CallbackQuery!.Data)
            {
                case "Назад":
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                default:
                    userState.UserData.CurrentFilter = new Filter(FilterType.Genre, update.CallbackQuery.Data);
                    return new ChoosingBookPage().View(update, userState);
            }
        }

        public PageResult View(Update update, UserState userState)
        {
            var text = @"Вот жанры, которые вы можете выбрать для чтения";
            var replyMarkup = GetReplyMarkup();

            userState.AddPage(this);
            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        private IReplyMarkup GetReplyMarkup()
        {
            return Book.CreateInlineKeyboardBooks(UserBooksStorage.Books.Select(x => x.Genre).ToHashSet());
        }
    }
}