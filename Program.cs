using MANIFESTA;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

internal class Program
{
    private readonly ITelegramBotClient _botClient = new TelegramBotClient("6558637896:AAFn-y5PLNvv_BfQhJEyhEWOPGItg3GCBX4");
    private static readonly KeyboardStateManager _keyboardStateManager = new();
    private static readonly Dictionary<long, string> _users = new();

    public async Task Run(string[] args)
    {
        var me = await _botClient.GetMeAsync();
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates =
                Array.Empty<
                    UpdateType>() // receive all update types except ChatMember related updates
        };
        using var cts = new CancellationTokenSource();
        _botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cts.Token);
        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();

        cts.Cancel();
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient client, Exception ex, CancellationToken token)
    {
        var ErrorMessage = ex switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => ex.ToString()
        };
        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }

    async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
    {
        if (update.Message is not { From.Id: var userId, Chat.Type: ChatType.Private } message)
            return;

        if (!_users.TryGetValue(userId, out string? currentStatus))
        {
            _users[userId] = currentStatus = "";
        }

        if (update.Message is { Text: { } messageText })
        {
            switch (messageText)
            {
                case "Юридичні.послуги🔮":
                    {
                        await LegalServ(bot, userId, ct);
                        break;
                    }

                case "Бізнес Аналітика🏅":
                    {
                        await Analytics(bot, userId, ct);
                        break;
                    }

                case "Бух.Послуги🌸":
                    {
                        await AccServ(bot, userId, ct);
                        break;
                    }


                case "Управлінський облік🐍":
                    {
                        await ManagAcc(bot, userId, ct);
                        break;
                    }

                case "Аналітика🏅":
                case "Юридичні🔮":
                case "Бухгалтерські🌸":
                case "Управлінські🐍":
                    await GetPhone(bot, userId, messageText);
                    break;
            }

            var currentKeyboard = _keyboardStateManager.GetCurrentKeyboard();
            switch (messageText)
            {
                case "Наші послуги🐣":
                    _keyboardStateManager.ShowSubmenu();
                    currentKeyboard = _keyboardStateManager.GetCurrentKeyboard();
                    break;

                case "Назад⏎":
                    _keyboardStateManager.HideSubmenu();
                    currentKeyboard = _keyboardStateManager.GetCurrentKeyboard();
                    break;

                case "Залишити заявку☎️":
                    _keyboardStateManager.ShowContactmenu();
                    currentKeyboard = _keyboardStateManager.GetContactKeyboard2();
                    await bot.SendTextMessageAsync(update.Message.Chat.Id, "Натисніть кнопку щоб поділитися контактом", cancellationToken: ct);
                    break;
            }

            _ = await bot.SendTextMessageAsync(
                chatId: userId,
                text: "Оберіть що вас цікавить",
                replyMarkup: currentKeyboard,
                cancellationToken: ct);

            Console.WriteLine($"Received a '{messageText}' message in chat {userId}.");
        }
        else if (update.Message is { Contact: { } contact })
        {
            string phone = contact.PhoneNumber;

            switch (currentStatus)
            {
                case "Аналітика🏅":
                    {
                        // Handle analytics                            
                        break;
                    }
                case "Юридичні🔮":
                    {
                        // Handle analytics
                        break;
                    }
                case "Бухгалтерські🌸":
                    {
                        // Handle analytics
                        break;
                    }
                case "Управлінські🐍":
                    {
                        // Handle analytics
                        break;
                    }
            }

            Console.WriteLine($"{phone}");
            Console.WriteLine("UKRAINE");
        }
    }

    async Task LegalServ(ITelegramBotClient bot, long userId, CancellationToken ct)
    {
        // HTML-formatted text with a link to the image
        var legalServicesText = "some message";

        // Send the formatted text message with HTML parse mode
        await bot.SendTextMessageAsync(userId, legalServicesText,
            parseMode: ParseMode.Html, cancellationToken: ct);
    }

    async Task Analytics(ITelegramBotClient telegramBotClient, long userId, CancellationToken cancellationToken)
    {
        await telegramBotClient.SendMediaGroupAsync(
            chatId: userId,
            media: new IAlbumInputMedia[]
            {
                new InputMediaPhoto(
                    InputFile.FromString("https://imgur.com/5VXtIcU")),
                new InputMediaPhoto(
                    InputFile.FromString("https://imgur.com/BdoOhTo")),
                new InputMediaPhoto(
                    InputFile.FromString("https://imgur.com/KqpGHDi"))
            },
            cancellationToken: cancellationToken);
    }

    async Task AccServ(ITelegramBotClient telegramBotClient, long userId, CancellationToken cancellationToken)
    {
        await telegramBotClient.SendPhotoAsync(userId,
            InputFile.FromString("https://imgur.com/TtZMndX"),
            allowSendingWithoutReply: true, cancellationToken: cancellationToken);
    }

    async Task ManagAcc(ITelegramBotClient telegramBotClient, long userId, CancellationToken cancellationToken)
    {
        await telegramBotClient.SendPhotoAsync(userId,
            InputFile.FromString("https://imgur.com/5eEglRY"),
            allowSendingWithoutReply: true, cancellationToken: cancellationToken);
    }

    async Task GetPhone(ITelegramBotClient telegramBotClient, long userId, string newStatus)
    {
        await telegramBotClient.SendTextMessageAsync(userId, "Введіть номер телефону",
            replyMarkup: new ReplyKeyboardMarkup(
                new KeyboardButton("Поділитися номером телефону") { RequestContact = true }));
        _users[userId] = newStatus;
    }
}