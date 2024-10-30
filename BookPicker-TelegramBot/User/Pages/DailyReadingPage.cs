using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class DailyReadingPage : IPage
    {
        public PageResult Handle(Update update, UserState userState)
        {
            switch (update.CallbackQuery.Data)
            {
                case "Назад":
                    return new StartPage().View(update, userState);
                default:
                    return null;
            }
        }

        public PageResult View(Update update, UserState userState)
        {
            var text = @"Вот книги, которые Вы добавили для ежедневного чтения:";
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

        public IReplyMarkup GetReplyMarkup()
        {
            return new InlineKeyboardMarkup(
                [
                    [
                        InlineKeyboardButton.WithCallbackData("Книга 1")
                    ],

                    [
                       InlineKeyboardButton.WithCallbackData("Книга 2")
                    ],

                    [
                        InlineKeyboardButton.WithCallbackData("Назад"),
                    ]
                ]);
        }
    }
}