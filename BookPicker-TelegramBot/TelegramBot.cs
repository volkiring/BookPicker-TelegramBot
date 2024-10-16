
using BookPicker_TelegramBot.Storage;
using BookPicker_TelegramBot.User;
using BookPicker_TelegramBot.User.Pages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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
        if (update.Type != Telegram.Bot.Types.Enums.UpdateType.CallbackQuery && update.Type != Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            return;
        }

        long telegramUserId;

        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            telegramUserId = update.CallbackQuery.From.Id;
        }
        else
        {
            telegramUserId = update.Message.From.Id;
        }

        Console.WriteLine($"update.Id={update.Id} telegramUserId={telegramUserId}");

        var userState = await storage.TryGetAsync(telegramUserId);

        if (userState == null)
        {
            userState = new UserState(new Stack<IPage>([new NotStatedPage()]), new UserData());
        }

        Console.WriteLine($"update.Id={update.Id}  currentState={userState}");

        var result = userState!.CurrentPage.Handle(update, userState);
        Console.WriteLine($"update.Id={update.Id} text={result.Text} updatedState={result.UpdatedUserState}");

        if (userState == null)
        {
            await client.SendTextMessageAsync(
                chatId: telegramUserId,
                text: result.Text,
                replyMarkup: result.ReplyMarkup,
                parseMode: result.ParseMode);
        }
        else
        {
            await client.EditMessageTextAsync(
            chatId: telegramUserId,
            messageId: update.CallbackQuery.Message.MessageId,
            text: result.Text,
            replyMarkup: (InlineKeyboardMarkup)result.ReplyMarkup,
            parseMode: result.ParseMode);
        }

        storage.AddOrUpdate(telegramUserId, result.UpdatedUserState);
    }
} 