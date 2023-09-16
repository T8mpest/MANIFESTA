using MANIFESTA;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

internal class Program
{
    //private readonly ITelegramBotClient _botClient = new TelegramBotClient("6558637896:AAFn-y5PLNvv_BfQhJEyhEWOPGItg3GCBX4");
    private static readonly Dictionary<long, string> _users = new();

    public static async Task Main(string[] args)
    {
        var botClient = new TelegramBotClient("6558637896:AAFn-y5PLNvv_BfQhJEyhEWOPGItg3GCBX4");
        var me = await botClient.GetMeAsync();
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates =
                Array.Empty<
                    UpdateType>() // receive all update types except ChatMember related updates
        };
        using var cts = new CancellationTokenSource();
        botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cts.Token);
        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();

        cts.Cancel();
    }

    static Task HandlePollingErrorAsync(ITelegramBotClient client, Exception ex, CancellationToken token)
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

    static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
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
                        return;
                    }

                case "Бізнес Аналітика🏅":
                    {
                        await Analytics(bot, userId, ct);
                        return;
                    }

                case "Бух.Послуги🌸":
                    {
                        await AccServ(bot, userId, ct);
                        return;
                    }

                case "Управлінський облік🐍":
                    {
                        await ManagAcc(bot, userId, ct);
                        return;
                    }

                case "Аналітика🏅":
                case "Юридичні🔮":
                case "Бухгалтерські🌸":
                case "Управлінські🐍":
                    await GetPhone(bot, userId, messageText);
                    return;
            }

            IReplyMarkup newKeyboard;
            switch (messageText)
            {
                case "Наші послуги🐣":
                    newKeyboard = Keyboards.GetSubmenuKeyboard;
                    break;
                case "Залишити заявку☎️":
                    newKeyboard = Keyboards.GetProductsKeyboard;
                    await bot.SendTextMessageAsync(update.Message.Chat.Id, "Натисніть кнопку щоб поділитися контактом", cancellationToken: ct);
                    break;
                default:
                    newKeyboard = Keyboards.GetMainKeyboard;
                    break;
            }

            _ = await bot.SendTextMessageAsync(
                chatId: userId,
                text: "Оберіть що вас цікавить",
                replyMarkup: newKeyboard,
                cancellationToken: ct);

            Console.WriteLine($"Received a '{messageText}' message in chat {userId}.");
        }
        else if (update.Message is { Contact: { } contact })
        {
            string Phone = contact.PhoneNumber;
            string FirstName = contact.FirstName; 
            string LastName = contact.LastName;
            switch (currentStatus)
            {
                case "Аналітика🏅":
                    {                        
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

            Console.WriteLine($"PhoneNum: {Phone},\nName: {FirstName}, LastName: {LastName}");

        }
    }

    static async Task LegalServ(ITelegramBotClient bot, long userId, CancellationToken ct)
    {
        // HTML-formatted text with a link to the image
        var legalServicesText = "some message";

        // Send the formatted text message with HTML parse mode
        await bot.SendTextMessageAsync(userId, legalServicesText,
            parseMode: ParseMode.Html, cancellationToken: ct);
    }

    static async Task Analytics(ITelegramBotClient telegramBotClient, long userId, CancellationToken cancellationToken)
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

    static async Task AccServ(ITelegramBotClient telegramBotClient, long userId, CancellationToken cancellationToken)
    {
        await telegramBotClient.SendPhotoAsync(userId,
            InputFile.FromString("https://imgur.com/TtZMndX"),
            allowSendingWithoutReply: true, cancellationToken: cancellationToken);
    }

    static async Task ManagAcc(ITelegramBotClient telegramBotClient, long userId, CancellationToken cancellationToken)
    {
        await telegramBotClient.SendPhotoAsync(userId,
            InputFile.FromString("https://imgur.com/5eEglRY"),
            allowSendingWithoutReply: true, cancellationToken: cancellationToken);
    }


    static async Task GetPhone(ITelegramBotClient telegramBotClient, long userId, string newStatus)
    {
        await telegramBotClient.SendTextMessageAsync(userId, "Введіть номер телефону",
            replyMarkup: Keyboards.GetContactKeyboard);
        _users[userId] = newStatus;
    }
}