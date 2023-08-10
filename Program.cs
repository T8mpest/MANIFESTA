using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bots.Types.Inline;
using MANIFESTA;

namespace MANIFESTA
{


    internal class Program
    {
        private static ITelegramBotClient botClient;
        private static InlineProccesor _inlineProcessor;

        static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient("6558637896:AAFn-y5PLNvv_BfQhJEyhEWOPGItg3GCBX4");
            _inlineProcessor = new InlineProccesor();
            var me = await botClient.GetMeAsync();
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };
            using CancellationTokenSource cts = new();
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();

        }


        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type == UpdateType.InlineQuery)
            {
                await _inlineProcessor.BotOnInlineQueryReceived(botClient, update.InlineQuery);
            }
            // Перевірка на ChosenInlineResult
            else if (update.Type == UpdateType.ChosenInlineResult)
            {
                await _inlineProcessor.BotOnChosenInlineResultReceived(botClient, update.ChosenInlineResult);
            }

            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");



            InlineKeyboardMarkup inlineKeyboard = new(new[]
        {
    InlineKeyboardButton.WithUrl(
        text: "Check our Inst:",
        url: "https://www.instagram.com/manifesta_consult/")
});

            Message sentMessaage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Ще більше інформації:",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
        new KeyboardButton("Наші послуги"),
        new KeyboardButton("Прайси"),
        KeyboardButton.WithRequestContact("Залишити заявку☎️"),
    });

            Message ssentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Оберіть що вас цікавить",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);
            //Echo received message text

            Console.WriteLine(
            $"{message.From.FirstName} sent message {message.MessageId} " +
            $"to chat {message.Chat.Id} at {message.Date.ToLocalTime}. " +
            $"It is a reply to message {message.ReplyToMessage.MessageId} " +
            $"and has {message.Entities.Length} message entities.");

        }

         static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}


