using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class BookOfMonthPage : IPage
    {


        public PageResult View(Update update, UserState userState)
        {
            var text = @"Вот книги, которые чаще всего пользователи добавляют в закладки:";

            var replyMarkup = GetReplyMarkup();
            userState.Pages.Push(this);
            return new PageResult(text, replyMarkup)
            {
                UpdatedUserState = userState
            };
        }

        public PageResult Handle(Update update, UserState userState)
        {
            switch (update.CallbackQuery.Data)
            {
                case "Назад":
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                default:
                    return null;
            }
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