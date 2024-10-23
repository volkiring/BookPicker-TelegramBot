
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
        if (update.Type != Telegram.Bot.Types.Enums.UpdateType.CallbackQuery && update.Type != Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            return;
        }

        UserState userState;

        if (update.Message != null && update.Message.Text == "/start")
        {
            userState = new UserState(new NotStatedPage(), new UserData()); // Переход на NotStatedPage
            long telegramUserId = update.Message.From.Id;

            Console.WriteLine($"update.Id={update.Id} telegramUserId={telegramUserId}");

            var IsExistUserState = storage.TryGet(telegramUserId, out userState);

            if (!IsExistUserState)
            {
                userState = new UserState(new NotStatedPage(), new UserData());
            }

            Console.WriteLine($"update.Id={update.Id} currentState={userState}");

            var result = userState!.Page.Handle(update, userState); // Обработка NotStatedPage

            Console.WriteLine($"update.Id={update.Id} text={result.Text} updatedState={result.UpdatedUserState}");

            if (!IsExistUserState)
            {
                await client.SendTextMessageAsync(
                    chatId: telegramUserId,
                    text: result.Text,
                    replyMarkup: result.ReplyMarkup,
                    parseMode: result.ParseMode);
            }
            else
            {
                var currentMessage = update.CallbackQuery?.Message;
                var newText = result.Text;
                var newMarkup = result.ReplyMarkup;

                if (currentMessage != null)
                {
                    bool isTextChanged = currentMessage.Text != newText;
                    bool isMarkupChanged = !currentMessage.ReplyMarkup.Equals(newMarkup);

                    if (isTextChanged || isMarkupChanged)
                    {
                        await client.EditMessageTextAsync(
                            chatId: telegramUserId,
                            messageId: currentMessage.MessageId,
                            text: newText,
                            replyMarkup: (InlineKeyboardMarkup)newMarkup,
                            parseMode: result.ParseMode);
                    }
                }
            }

            storage.AddOrUpdate(telegramUserId, result.UpdatedUserState); // Сохранение состояния
        }

        else if (update.CallbackQuery != null)
        {
            long telegramUserId = update.CallbackQuery.From.Id;

            Console.WriteLine($"update.Id={update.Id} telegramUserId={telegramUserId}");

            var IsExistUserState = storage.TryGet(telegramUserId, out userState);

            if (!IsExistUserState)
            {
                userState = new UserState(new NotStatedPage(), new UserData()); // Переход на NotStatedPage по умолчанию
            }

            Console.WriteLine($"update.Id={update.Id} currentState={userState}");

            var result = userState!.Page.Handle(update, userState);

            Console.WriteLine($"update.Id={update.Id} text={result.Text} updatedState={result.UpdatedUserState} current={userState.UserData.CurrentStatus.Item1}{userState.UserData.CurrentStatus.Item2}");

            var currentMessage = update.CallbackQuery.Message;
            var newText = result.Text;
            var newMarkup = result.ReplyMarkup;

            bool isTextChanged = currentMessage.Text != newText;
            bool isMarkupChanged = !currentMessage.ReplyMarkup.Equals(newMarkup);

            if (isTextChanged || isMarkupChanged)
            {
                await client.EditMessageTextAsync(
                    chatId: telegramUserId,
                    messageId: currentMessage.MessageId,
                    text: newText,
                    replyMarkup: (InlineKeyboardMarkup)newMarkup,
                    parseMode: result.ParseMode);
            }

            storage.AddOrUpdate(telegramUserId, result.UpdatedUserState);
        }
    }



}