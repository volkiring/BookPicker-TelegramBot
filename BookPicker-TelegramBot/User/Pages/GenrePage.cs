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
                    return new ChooseWay().View(update, userState);
                default:
                    userState.UserData.CurrentFilter = new Filter(FilterType.Genre, update.CallbackQuery.Data);
                    return new ChoosingBookPage().View(update, userState);
            }
        }

        public PageResult View(Update update, UserState userState)
        {
            var text = @"Вот жанры, которые вы можете выбрать для чтения";
            var replyMarkup = GetReplyMarkup();

            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        private IReplyMarkup GetReplyMarkup()
        {
            return Book.CreateInlineKeyboardBooks(UserBooksStorage.Books.Select(x => x.Genre).ToHashSet());
        }
    }
}