using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class StartPage : IPage
    {
        public PageResult View(Update update, UserState userState)
        {
            var text = @"Здравствуйте\!
Выберите действие, которое хотите совершить:";

            var replyMarkup = GetReplyMarkup();

            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public PageResult Handle(Update update, UserState userState)
        {
            if (update.CallbackQuery.Data == "Книги месяца")
            {
                return new BookOfMonthPage().View(update, userState);
            }

            if (update.CallbackQuery.Data == "Выбрать книгу")
            {
                return new ChoosingBookPage().View(update, userState);
            }

            if (update.CallbackQuery.Data == "Закладки")
            {
                return new BookmatePage().View(update, userState);
            }

            if (update.CallbackQuery.Data == "Ежедневное чтение")
            {
                return new DailyReadingPage().View(update, userState);
            }
            return null;
        }

        public IReplyMarkup GetReplyMarkup()
        {
            return new InlineKeyboardMarkup(
                [
                    [
                        InlineKeyboardButton.WithCallbackData("Книги месяца")
                    ],

                    [
                       InlineKeyboardButton.WithCallbackData("Выбрать книгу")
                    ],

                    [
                        InlineKeyboardButton.WithCallbackData("Закладки"),
                         InlineKeyboardButton.WithCallbackData("Ежедневное чтение")
                    ]
                ]);
        }
    }
}

