﻿using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class StartPage : IPage
    {
        public PageResult View(Update update, UserState userState)
        {
            var text = GetText();

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
                case "Книги месяца":
                    return new BookOfMonthPage().View(update, userState);
                case "Выбрать книгу":
                    return new ChooseWay().View(update, userState);
                case "Закладки":
                    return new BookmarksPage().View(update, userState);
                case "Ежедневное чтение":
                    return new ReminderPage().View(update, userState);
                default:
                    return null;
            }
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

        public string GetText()
        {
            return @"Здравствуйте\!
Выберите действие, которое хотите совершить:";
        }
    }
}

