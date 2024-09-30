using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class StartPage : IPage
    {
        public PageResult View(Update update, UserState userState)
        {
            var text = @"Здравствуйте!
Выберите действие, которое хотите совершить:";

            var replyMarkup = GetReplyMarkup();

            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = new UserState(this, userState.UserData)
            };
        }

        public ReplyKeyboardMarkup GetReplyMarkup()
        {
            return new ReplyKeyboardMarkup(
                [
                    [
                        new KeyboardButton("Книги месяца")
                    ],

                    [
                        new KeyboardButton("Выбрать книгу")
                    ],

                    [
                        new KeyboardButton("Закладки"),
                         new KeyboardButton("Ежедневное чтение")
                    ]
                ])
            {
                ResizeKeyboard = true
            };
        }

        public PageResult Handle(Update update, UserState userState)
        {
            if (update.Message.Text == null)
            {
                return new PageResult("Нажмите на кнопки", GetReplyMarkup());
            }

            if (update.Message.Text == "Книги месяца")
            {
                return new BookOfMonthPage().View(update, userState);
            }

            return null;
        }
    }
}
