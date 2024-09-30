
using BookPicker_TelegramBot.Storage;
using BookPicker_TelegramBot.User;
using BookPicker_TelegramBot.User.Pages;
using Telegram.Bot;
using Telegram.Bot.Types;

public class Program
{
    static UserStateStorage storage = new UserStateStorage();

    static async Task Main(string[] args)
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
        if (update.Message?.Text == null)
        {
            return;
        }

        long telegramUserId = update.Message.From.Id;
        Console.WriteLine($"update.Id={update.Id} telegramUserId={telegramUserId}");

        var IsExistUserState = storage.TryGet(telegramUserId, out var userState);

        if (!IsExistUserState)
        {
            userState = new UserState(new NotStatedPage(), new UserData());
        }
        Console.WriteLine($"update.Id={update.Id}  currentState={userState}");

        var result = userState!.Page.Handle(update, userState);
        Console.WriteLine($"update.Id={update.Id} text={result.Text} updatedState={result.UpdatedUserState}");


        await client.SendTextMessageAsync(
            chatId: telegramUserId,
            text: result.Text,
            replyMarkup: result.ReplyMarkup
            );

        storage.AddOrUpdate(telegramUserId, result.UpdatedUserState);
    }
}