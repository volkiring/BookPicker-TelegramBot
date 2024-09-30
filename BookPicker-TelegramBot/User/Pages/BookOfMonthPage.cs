using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class BookOfMonthPage : IPage
    {
        public PageResult Handle(Update update, UserState Userstate)
        {
            throw new NotImplementedException();
        }

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
                        new KeyboardButton("Книга 1")
                   ],

                   [
                        new KeyboardButton("Книга 2")
                   ]
              ])
            {
                ResizeKeyboard = true
            };
        }
    }
}