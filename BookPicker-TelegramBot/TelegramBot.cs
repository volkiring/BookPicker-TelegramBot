
using BookPicker_TelegramBot.Storage;
using BookPicker_TelegramBot.User;
using BookPicker_TelegramBot.User.Pages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class Program
{
    static UserStateStorage storage = new UserStateStorage();

    static async Task Main()
    {
        string token = "8142383621:AAGGxzdxmaj-gUtZ-pNtD6HMYR1YiJsNJ7I";
        TelegramBotClient telegramBotClient = new TelegramBotClient(
            token: token
            );
        var me = telegramBotClient.GetMeAsync().Result;
        telegramBotClient.StartReceiving(
            updateHandler: UpdateHandler,
            pollingErrorHandler: ErrorHandler
            );
        Console.WriteLine($"Бот запущен : {me}");
        Console.ReadKey();

    }

    static async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
    }

    static async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Type != Telegram.Bot.Types.Enums.UpdateType.CallbackQuery &&
            update.Type != Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            return;
        }

        UserState userState;
        long telegramUserId = update.Message?.From.Id ?? update.CallbackQuery.From.Id;
        bool isMessageUpdate = update.Message != null && update.Message.Text == "/start";
        bool isCallbackQuery = update.CallbackQuery != null;

        if (isMessageUpdate || isCallbackQuery)
        {
            Console.WriteLine($"update.Id={update.Id} telegramUserId={telegramUserId}");

            bool isUserStateExist = storage.TryGet(telegramUserId, out userState);

            if (!isUserStateExist)
            {
                userState = new UserState(new Stack<IPage>([new NotStatedPage()]), new UserData());
                storage.AddOrUpdate(telegramUserId, userState); 
            }

            var result = userState.CurrentPage.Handle(update, userState);
            Console.WriteLine($"update.Id={update.Id} text={result.Text} updatedState={result.UpdatedUserState}");


            if (isMessageUpdate)
            {
                await client.SendTextMessageAsync(
                    chatId: telegramUserId,
                    text: result.Text,
                    replyMarkup: result.ReplyMarkup,
                    parseMode: result.ParseMode);
            }
            else if (isCallbackQuery)
            {
                
                var currentMessage = update.CallbackQuery.Message;
                if (currentMessage != null)
                {
                    bool isTextChanged = currentMessage.Text != result.Text;
                    bool isMarkupChanged = currentMessage.ReplyMarkup == null || !currentMessage.ReplyMarkup.Equals(result.ReplyMarkup);


                    if (isTextChanged || isMarkupChanged)
                    {
                        await client.EditMessageTextAsync(
                            chatId: telegramUserId,
                            messageId: currentMessage.MessageId,
                            text: result.Text,
                            replyMarkup: (InlineKeyboardMarkup)result.ReplyMarkup,
                            parseMode: result.ParseMode);
                    }
                }
            }

            storage.AddOrUpdate(telegramUserId, result.UpdatedUserState);
        }
    }



}