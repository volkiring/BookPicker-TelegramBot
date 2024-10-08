﻿using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookPicker_TelegramBot.User.Pages
{
    public class PageResult
    {
        public string Text { get; set; }

        public IReplyMarkup ReplyMarkup { get; set; }

        public UserState UpdatedUserState { get; set; }

        public ParseMode ParseMode { get; set; } = ParseMode.MarkdownV2;
        public PageResult(string text, IReplyMarkup replyMarkup)
        {
            Text = text;
            ReplyMarkup = replyMarkup;
        }
    }
}