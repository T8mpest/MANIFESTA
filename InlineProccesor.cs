using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MANIFESTA
{
    internal class InlineProccesor
    {

        private readonly string[] sites = { "Google", "Github", "Telegram", "Wikipedia" };
        private readonly string[] siteDescriptions =
        {
    "Google is a search engine",
    "Github is a git repository hosting",
    "Telegram is a messenger",
    "Wikipedia is an open wiki"
};
        public async Task BotOnInlineQueryReceived(ITelegramBotClient bot, InlineQuery inlineQuery)
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
        public Task BotOnChosenInlineResultReceived(ITelegramBotClient bot, ChosenInlineResult chosenInlineResult)
        {
            if (uint.TryParse(chosenInlineResult.ResultId, out var resultId) // check if a result id is parsable and introduce variable
                && resultId < sites.Length)
            {
                Console.WriteLine($"User {chosenInlineResult.From} has selected site: {sites[resultId]}");
            }

            return Task.CompletedTask;
        }
    }
}
