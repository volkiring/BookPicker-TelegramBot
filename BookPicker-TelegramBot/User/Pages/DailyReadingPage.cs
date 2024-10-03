using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class DailyReadingPage : IPage
    {
        public PageResult Handle(Update update, UserState userState)
        {
            if (update.CallbackQuery.Data == "Назад")
            {
                return new StartPage().View(update, userState);
            }
            return null;
        }

        public PageResult View(Update update, UserState userState)
        {
            var text = @"Вот книги, которые Вы добавили для ежедневного чтения:";
            var replyMarkup = GetReplyMarkup();

            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
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