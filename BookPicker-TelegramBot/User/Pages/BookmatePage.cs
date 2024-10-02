using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class BookmatePage : IPage
    {
        public PageResult Handle(Update update, UserState Userstate)
        {
            throw new NotImplementedException();
        }

        public PageResult View(Update update, UserState userState)
        {
            throw new NotImplementedException();
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