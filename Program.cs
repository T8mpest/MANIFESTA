using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using MANIFESTA;
using Telegram.Bot.Types.InlineQueryResults;
using System.Threading;
using System;

namespace MANIFESTA
{


    internal class Program
    {
        private readonly ITelegramBotClient botClient = new TelegramBotClient("6558637896:AAFn-y5PLNvv_BfQhJEyhEWOPGItg3GCBX4");
        private static KeyboardStateManager _keyboardStateManager = new();
        private static int flag = 0;

        public async Task Run(string[] args)
        {

            var me = await botClient.GetMeAsync();
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<Telegram.Bot.Types.Enums.UpdateType>() // receive all update types except ChatMember related updates
            };
            using var cts = new CancellationTokenSource();
            botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cts.Token);
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
            var users = new Dictionary<long, string>();
            if (update.Message is not { } message)
                return;

            if (update.Message is { Text: { } messageTextt })
            {
                var chatId = message.Chat.Id;
                if (update.Message is { Text: { } messageText })
                {
                    switch (messageText)
                    {
                        case "Юридичні.послуги🔮":
                            {
                                await LegalServ(bot, update, ct);
                                break;
                            }

                        case "Бізнес Аналітика🏅":
                            {
                                await Analytics(bot, chatId, ct);
                                break;
                            }

                        case "Бух.Послуги🌸":
                            await AccServ(bot, update, ct);
                            break;

                        case "Управлінський облік🐍":
                            await ManagAcc(bot, update, ct);
                            break;

                        case "Аналітика🏅":
                        case "Юридичні🔮":
                        case "Бухгалтерські🌸":
                        case "Управлінські🐍":
                            await Getphone(bot, update, users, message, messageText);
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
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, "Натисніть кнопку щоб поділитися контактом");
                            break;
                    }

                    _ = await bot.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Оберіть що вас цікавить",
                        replyMarkup: currentKeyboard,
                        cancellationToken: ct);

                    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
                }
            }
            else if (update.Message is { Contact: { } contact })
            {
                string phone = contact.PhoneNumber;
                string action = users[update.Message.From.Id];

                switch (action)
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

        async Task LegalServ(ITelegramBotClient bot, Update update, CancellationToken ct)
        {
            // HTML-formatted text with a link to the image
            var legalServicesText = "some message";

            // Send the formatted text message with HTML parse mode
            await bot.SendTextMessageAsync(update.Message.Chat.Id, legalServicesText,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html, cancellationToken: ct);
        }

        async Task Analytics(ITelegramBotClient telegramBotClient, long chatId, CancellationToken cancellationToken)
        {
            await telegramBotClient.SendMediaGroupAsync(
                chatId: chatId,
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

        async Task AccServ(ITelegramBotClient telegramBotClient, Update update1, CancellationToken cancellationToken)
        {

            await telegramBotClient.SendPhotoAsync(update1.Message.Chat.Id, InputFile.FromString("https://imgur.com/TtZMndX"),
                allowSendingWithoutReply: true, cancellationToken: cancellationToken);
        }

        async Task ManagAcc(ITelegramBotClient telegramBotClient, Update update1, CancellationToken cancellationToken)
        {

            await telegramBotClient.SendPhotoAsync(update1.Message.Chat.Id, InputFile.FromString("https://imgur.com/5eEglRY"),
                allowSendingWithoutReply: true, cancellationToken: cancellationToken);
        }

        async Task Getphone(ITelegramBotClient telegramBotClient, Update update1, Dictionary<long, string>? dictionary, Message message1,
            string s)
        {
           // var currentKeyboard =
            await telegramBotClient.SendTextMessageAsync(update1.Message.Chat, "Введіть номер телефону",
                replyMarkup: new ReplyKeyboardMarkup(
                    new KeyboardButton("Поділитися номером телефону") { RequestContact = true }));
            dictionary[message1.From.Id] = s;
        }

    }
}







