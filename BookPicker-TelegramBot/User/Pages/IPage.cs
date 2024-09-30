using Telegram.Bot.Types;

namespace BookPicker_TelegramBot.User.Pages
{
    public interface IPage
    {
        PageResult View(Update update, UserState userState); 

        PageResult Handle(Update update, UserState Userstate);
    }
}