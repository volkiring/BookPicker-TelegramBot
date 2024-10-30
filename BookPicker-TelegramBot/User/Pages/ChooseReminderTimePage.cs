using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class ChooseReminderTimePage : IPage
    {
        public PageResult Handle(Update update, UserState userState)
        {
            switch (update!.CallbackQuery!.Data)
            {
                case "Назад":
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                case "10:00":
                    userState.UserData.Reminders.Add(new Reminder(userState.UserData.CurrentBook, new TimeSpan(10, 00, 0)));
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                case "13:00":
                    userState.UserData.Reminders.Add(new Reminder(userState.UserData.CurrentBook, new TimeSpan(13, 00, 0)));
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                case "16:00":
                    userState.UserData.Reminders.Add(new Reminder(userState.UserData.CurrentBook, new TimeSpan(16, 00, 0)));
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                case "19:00":
                    userState.UserData.Reminders.Add(new Reminder(userState.UserData.CurrentBook, new TimeSpan(19, 00, 0)));
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);
                default:
                    userState.Pages.Pop();
                    return userState.CurrentPage.View(update, userState);

            }
        }

        public PageResult View(Update update, UserState userState)
        {
            var text = @"Выберите время, в которое вам будет приходить упоминание";
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
                        InlineKeyboardButton.WithCallbackData("10:00"),
                        InlineKeyboardButton.WithCallbackData("13:00"),
                        InlineKeyboardButton.WithCallbackData("16:00"),
                        InlineKeyboardButton.WithCallbackData("19:00")
                    ],

                    [
                        InlineKeyboardButton.WithCallbackData("Назад"),
                    ]
                ]);
        }
    }
}
