using BookPicker_TelegramBot.Storage;
using BookPicker_TelegramBot.User;
using BookPicker_TelegramBot.User.Pages;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class Program
{
    static UserStateStorage storage = new UserStateStorage();

    static List<UserState> allUsers = storage.GetAllUsersAsync().Result;

    static string token = "8142383621:AAGGxzdxmaj-gUtZ-pNtD6HMYR1YiJsNJ7I";

    static TelegramBotClient telegramBotClient = new TelegramBotClient(
        token: token
        );
    static async Task Main()
    {
        var me = telegramBotClient.GetMeAsync().Result;
        telegramBotClient.StartReceiving(
            updateHandler: UpdateHandler,
            pollingErrorHandler: ErrorHandler
            );
        Console.WriteLine($"Бот запущен : {me}");
        await SendReminder();
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
        bool IsExistInData = true;

        if (userState == null)
        {
            IsExistInData = false;
            userState = new UserState(new Stack<IPage>([new NotStatedPage()]), new UserData(telegramUserId));
        }

        Console.WriteLine($"update.Id={update.Id}  currentState={userState}");

        var result = userState!.CurrentPage.Handle(update, userState);
        Console.WriteLine($"update.Id={update.Id} text={result.Text} updatedState={result.UpdatedUserState}");

        if (!IsExistInData)
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
            messageId: update!.CallbackQuery!.Message!.MessageId,
            text: result.Text,
            replyMarkup: (InlineKeyboardMarkup)result.ReplyMarkup,
            parseMode: result.ParseMode);
        }

        storage.AddOrUpdate(telegramUserId, result.UpdatedUserState);
    }


    public static async Task SendReminder()
    {
        while (true)
        {
            foreach (var user in allUsers)
            {
                var reminders = user.UserData.Reminders;
                foreach (var reminder in reminders)
                {
                    var currentTime = DateTime.Now.TimeOfDay;
                    var notificationTime = reminder.Time;
                    var text = $"Время прочитать книгу автора {reminder.Book.Author} {reminder.Book.Title}";
                    var link = reminder.Book.LinkToRead;



                    if (currentTime.Hours == notificationTime.Hours && currentTime.Minutes == notificationTime.Minutes)
                    {
                        try
                        {
                            await telegramBotClient.SendTextMessageAsync(
                                chatId: user.UserData.Id,
                                text: text,
                                replyMarkup: new InlineKeyboardMarkup(
                                    InlineKeyboardButton.WithUrl("Читать далее", link)
                                )
                            );
                            Console.WriteLine("Сообщение отправлено!");

                            await Task.Delay(TimeSpan.FromMinutes(1));
                        }
                        catch (ApiRequestException apiEx)
                        {
                            Console.WriteLine($"Ошибка API Telegram: {apiEx.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Произошла ошибка: {ex.Message}");
                        }
                        await Task.Delay(1000);
                    }

                }
            }
        }
    }
}