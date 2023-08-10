using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using MANIFESTA;
using Telegram.Bot.Types.InlineQueryResults;

namespace MANIFESTA
{


    internal class Program
    {
        private readonly ITelegramBotClient botClient = new TelegramBotClient("6558637896:AAFn-y5PLNvv_BfQhJEyhEWOPGItg3GCBX4");      
        private static InlineProccesor _inlineProcessor;
        private readonly string[] sites = { "Google", "Github", "Telegram", "Wikipedia" };
        private readonly string[] siteDescriptions =
        {
    "Google is a search engine",
    "Github is a git repository hosting",
    "Telegram is a messenger",
    "Wikipedia is an open wiki"
};
        public async Task Run(string[] args)
        {           
            
            _inlineProcessor = new InlineProccesor();
            var me = await botClient.GetMeAsync();
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };
            using var cts = new CancellationTokenSource();
            botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cts.Token);
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();

        }


         async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
        {
            try
            {
                // ANCHOR: switch-statement
                await (update.Type switch
                {
                    UpdateType.InlineQuery => BotOnInlineQueryReceived(bot, update.InlineQuery!),
                    UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(bot, update.ChosenInlineResult!),
                    UpdateType.Message => HandleMessageUpdate(bot, update.Message, ct!), // Добавьте эту строку
                    _ => Task.CompletedTask // Добавьте это для обработки остальных случаев
                });
                // ANCHOR_END: switch-statement
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while handling {update.Type}: {ex}");
            }

        }
        static async Task HandleMessageUpdate(ITelegramBotClient bot, Message message, CancellationToken ct)
        {
            if (message is not { Text: { } messageText }) // Проверка на наличие текста в сообщении
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
        InlineKeyboardButton.WithUrl(
            text: "Check our Inst:",
            url: "https://www.instagram.com/manifesta_consult/")
    });

            Message sentMessage = await bot.SendTextMessageAsync(
                chatId: chatId,
                text: "Ще більше інформації:",
                replyMarkup: inlineKeyboard,
                cancellationToken: ct);

            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
        new KeyboardButton("Наші послуги"),
        new KeyboardButton("Прайси"),
        KeyboardButton.WithRequestContact("Залишити заявку☎️"),
    });

            Message ssentMessage = await bot.SendTextMessageAsync(
                chatId: chatId,
                text: "Оберіть що вас цікавить",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: ct);

            Console.WriteLine(
                $"{message.From.FirstName} sent message {message.MessageId} " +
                $"to chat {message.Chat.Id} at {message.Date.ToLocalTime}. " +
                $"It is a reply to message {message.ReplyToMessage?.MessageId} " +
                $"and has {message.Entities?.Length ?? 0} message entities.");
        }
        async Task BotOnInlineQueryReceived(ITelegramBotClient bot, Telegram.Bot.Types.InlineQuery inlineQuery)
        {
            var results = new List<InlineQueryResult>();

            var counter = 0;
            foreach (var site in sites)
            {
                results.Add(new InlineQueryResultArticle(
                    $"{counter}", // we use the counter as an id for inline query results
                    site, // inline query result title
                    new InputTextMessageContent(siteDescriptions[counter])) // content that is submitted when the inline query result title is clicked
                );
                counter++;
            }

            await bot.AnswerInlineQueryAsync(inlineQuery.Id, results); // answer by sending the inline query result list
        }
        Task BotOnChosenInlineResultReceived(ITelegramBotClient bot, ChosenInlineResult chosenInlineResult)
        {
            if (uint.TryParse(chosenInlineResult.ResultId, out var resultId) // check if a result id is parsable and introduce variable
                && resultId < sites.Length)
            {
                Console.WriteLine($"User {chosenInlineResult.From} has selected site: {sites[resultId]}");
            }

            return Task.CompletedTask;
        }

          Task HandlePollingErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken ct)
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
    }
}


