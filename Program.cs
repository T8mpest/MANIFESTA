﻿using MANIFESTA;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

internal class Program
{
    // Creating dictionary for users
    private static readonly Dictionary<long, string> _users = new();

    public static async Task Main(string[] args)
    {
        // Start for bot
        var botClient = new TelegramBotClient("YOUR API TOKEN");
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
        // Default ErrorHandler for tg bots
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
                //info when buttons click
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
                    // switch case for buttons to call method GetPhone
                case "Аналітика🏅":
                case "Юридичні🔮":
                case "Бухгалтерські🌸":
                case "Управлінські🐍":
                    await GetPhone(bot, userId, messageText);
                    return;
            }

            IReplyMarkup newKeyboard;
            // switch case for actions if buttons click
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
            // creating string for save the info from contact
            string Phone = contact.PhoneNumber;
            string FirstName = contact.FirstName; 
            string LastName = contact.LastName;
            switch (currentStatus)
            {
                case "Аналітика🏅":
                    {
                         await bot.SendTextMessageAsync(chatId: 1424766439, $"Номер телефону: {Phone},\n Ім'я: {FirstName}, Прізвище: {LastName}\n Зацікавлен у послугах аналітики.", cancellationToken: ct);
                        break;
                    }

                case "Юридичні🔮":
                    {
                        await bot.SendTextMessageAsync(chatId: 1823796239, $"Номер телефону: {Phone},\n Ім'я: {FirstName}, Прізвище: {LastName}\n Зацікавлен у Юридичних послугах.", cancellationToken: ct);
                        break;
                    }

                case "Бухгалтерські🌸":
                    {
                        await bot.SendTextMessageAsync(chatId: 1523756439, $"Номер телефону: {Phone},\n Ім'я: {FirstName}, Прізвище: {LastName}\n Зацікавлен у Бухгалтерськіх послугах.", cancellationToken: ct);
                        break;
                    }

                case "Управлінські🐍":
                    {
                        await bot.SendTextMessageAsync(chatId: 1422116439, $"Номер телефону: {Phone},\n Ім'я: {FirstName}, Прізвище: {LastName}\n Зацікавлен у послугах управління.", cancellationToken: ct);
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
        // task for Analytics Button
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
        // task for AccServ Button
        await telegramBotClient.SendPhotoAsync(userId,
            InputFile.FromString("https://imgur.com/TtZMndX"),
            allowSendingWithoutReply: true, cancellationToken: cancellationToken);
    }

    static async Task ManagAcc(ITelegramBotClient telegramBotClient, long userId, CancellationToken cancellationToken)
    {
        // task for ManagAcc Button
        await telegramBotClient.SendPhotoAsync(userId,
            InputFile.FromString("https://imgur.com/5eEglRY"),
            allowSendingWithoutReply: true, cancellationToken: cancellationToken);
    }


    static async Task GetPhone(ITelegramBotClient telegramBotClient, long userId, string newStatus)
    {
        // task for Get phone from contact using GetContactKeyboard + add usedId in dictionary
        await telegramBotClient.SendTextMessageAsync(userId, "Натисніть на кнопку",
            replyMarkup: Keyboards.GetContactKeyboard);
        _users[userId] = newStatus;
    }
}