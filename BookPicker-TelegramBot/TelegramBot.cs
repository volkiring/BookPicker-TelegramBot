
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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
            await MainMenu(client, chatId);
        }
    }
}

async Task MainMenu(ITelegramBotClient client, long chatId)
{
    InlineKeyboardMarkup inlineKeyboard = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Книги месяца", callbackData: "booksOfMonth")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Выбрать книгу", callbackData: "chooseBook")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Закладки", callbackData: "bookmarks"),
            InlineKeyboardButton.WithCallbackData(text: "Ежедневное чтение", callbackData: "everydayReading"),
        }
    });
    await client.SendTextMessageAsync(
        chatId: chatId,
        text: "Добро пожаловать! Выберите действие, которое хотите совершить:",
        replyMarkup: inlineKeyboard
        );
}
