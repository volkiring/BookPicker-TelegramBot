
using System.Diagnostics.Metrics;
using Telegram.Bot;
using Telegram.Bot.Types;

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

async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
{
    Console.WriteLine(exception.Message);
}

async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
{
    long chatId = update.Message.Chat.Id;
    if (update.Message.Text != null)
    {
        if (update.Message.Text == "/start")
        {
            await client.SendTextMessageAsync(chatId, "");
        }
    }
}
