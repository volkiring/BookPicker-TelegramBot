using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class ChooseWay : IPage
    {

        public PageResult View(Update update, UserState userState)
        {
            var text = @"Выберите, по какому критерию будете
выбирать книгу::";

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

        public PageResult Handle(Update update, UserState userState)
        {
            switch (update!.CallbackQuery!.Data)
            {
                case "Назад":
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                case "Выбрать книгу по автору":
                    return new AuthorsPage().View(update, userState);
                case "Выбрать книгу по жанру":
                    return new GenrePage().View(update, userState); 
                default:
                    return null;
            }
        }

        public IReplyMarkup GetReplyMarkup()
        {
            return new InlineKeyboardMarkup(
                [
                    [
                        InlineKeyboardButton.WithCallbackData("Выбрать книгу по автору")
                    ],

                    [
                       InlineKeyboardButton.WithCallbackData("Выбрать книгу по жанру")
                    ],

                    [
                        InlineKeyboardButton.WithCallbackData("Назад"),
                    ]
                ]);
        }

    }
}