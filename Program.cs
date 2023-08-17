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
        private static int instagramMessageSentCount = 0;
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

        private static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
        {
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;
            var chatId = message.Chat.Id;
            if (update.Message is { Text: { } messageTextt })
            {
                if (messageText == "Юридичні.послуги🔮")
                {
                    // HTML-formatted text with a link to the image
                    var legalServicesText = "<b>Юридичні послуги</b>\n" +
                                            " <b>1. Договори, контракти: </b> розробка договорів, контрактів, зокрема ЗЕД, угод, оцінка ризиків, коригування, підготовка протоколів розбіжностей," +
                                            "\r\nпогодження/переговори з контрагентом щодо\r\nправок.<i> \r\nУ процесі ведення господарської діяльності будь-якого підприємства виникають правовідносини з контрагентами, <b>у звʼязку з чим виникає необхідність укладання різного роду договорів, а саме:" +
                                            "\r\n купівлі-продажу\r\nоренди, суборенди\r\n>постачання на надання послуг, виконання робіт\r\nЗЕД-контрактів\r\nдодаткових угод про зміну умов договору.</b> продовження терміну договору тощо." +
                                            "\r\nОцінка ризиків - аналіз умов договорів щодо відповідності нормам законодавства та інтересів сторони, виявлення несприятливих умов, які можуть спричинити у себе розбіжності з контрагентом у майбутньому." +
                                            "\r\nКоригування та підготовка протоколів розбіжностей,\r\nпогодження/переговори з контрагентом щодо правок -" +
                                            "\r\n<b>комплекс послуг із внесення змін та правок до договорів відповідно до ваших побажань та інтересів,</b> подальше складання протоколу зі змінами та їх обговорення з контрагентом." +
                                            "\n  </i> <b> 2. Підготовка листів, скарг, відповідей у державні органи, органи місцевого самоврядування, підприємства та організації.</b>  <i>\r\nЦей блок охоплює надання юридичних послуг у процесі комунікації з державними органами, " +
                                            "органами місцевого самоврядування, підприємствами та організаціями, та передбачає такі послуги:\r\n﻿﻿листи, скарги, відповіді на адресу правоохоронних, податкових органів, прокуратури, інших контролюючих державних органів, банківських установ тощо;" +
                                            "\r\n﻿﻿підготовка різного роду листів контрагентам про оплату товару або послуги, відстрочення оплати, допостачання товару, неможливість поставки або виконання зобовʼязань за договором та інших листів" +
                                            "\r\n</i> <b>3. Розробка внутрішніх документів підприємства: накази, розпорядження, інструкції, становища; протоколи/рішення загальних зборів учасників/\r\nвищого органу (для ТОВ, ТДВ, ПП)</b>" +
                                            "\r\n<i>Чітка та злагоджена робота підприємства багато в чому залежить від наявності коректно прописаних внутрішніх документів,<b> тому ми надаємо послуги з розробки досить великого спектру внутрішніх документів підприємства:</b>" +
                                            "\r\n<b>﻿﻿накази:\r\n﻿﻿розпорядження;\r\n﻿﻿інструкції;\r\n﻿﻿становища;\r\n﻿﻿протоколи/рішення </b> загальних зборів учасників/ вищого органу про зміну директора, про зміну складу учасників підприємства, про доповнення або зміну переліку видів діяльності, про зміну розташування </i> " +
                                            "\r\n<b> 4. Консультації:</b> <i> короткі та розгорнуті (юридичні\r\nвисновки).\r\nВисокий рівень теоретичних знань, підкріплених практичними навичками, дозволяє нашим спеціалістам надати консультації в багатьох сферах діяльності в різних формах:" +
                                            "\r\n﻿﻿короткі консультації щодо окремо взятої ситуації;\r\n﻿﻿розгорнуті (юридичні укладання), що стосуються низки питань діяльності підприємства чи проекту." +
                                            "\r\n</i> <b>5. Претензійно-позовна робота: підготовка вимог/ претензій щодо оплати заборгованості</b>;<i> підготовка позовів, процесуальних документів, супроводження\r\nвиконавчого провадження" +
                                            "\r\n</i> <b>Захист прав та інтересів підприємства нерідко призводить до необхідності звернення до органів судової влади, наша компанія допоможе</b> підготовці різноманітних документів:" +
                                            "\r\n﻿﻿підготовка досудових вимог/претензій щодо оплати заборгованості, вимог виконання зобовʼязань контрагента згідно з договором;\r\n﻿﻿підготовка позовів про стягнення заборгованості, визнання неправомірними дій державних органів та Ін.;" +
                                            "\r\n﻿﻿підготовка процесуальних документів: відгуків, відповідей на відгук, клопотань, заяв, скарг;\r\n﻿﻿супровід виконавчого провадження: підготовка заяв про примусове виконання рішення, листів до органів виконавчої служби,\n" +
                                            "<a href=\"https://manifesta.com.ua/\">Наш сайт</a>";

                    // Send the formatted text message with HTML parse mode
                    await bot.SendTextMessageAsync(update.Message.Chat.Id, legalServicesText, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html, cancellationToken: ct);
                    instagramMessageSentCount++;
                }
                else if (messageText == "Бізнес Аналітика🏅")
                {
                    var legalServicesText = "<b>Бізнес Аналітика</b>\n" + // условно
                                            " НАЛАГОДЖЕННЯ ТА РОЗРОБКА BAS ";
                    await bot.SendMediaGroupAsync(
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
                            cancellationToken: ct);
                    instagramMessageSentCount++;
                    //await bot.SendPhotoAsync(update.Message.Chat.Id,InputFile.FromString("https://imgur.com/a/52f6Qkn"), allowSendingWithoutReply: true, cancellationToken: ct);
                }
                else if (messageText == "Бух.Послуги🌸")
                {
                    instagramMessageSentCount++;
                    await bot.SendPhotoAsync(update.Message.Chat.Id, InputFile.FromString("https://imgur.com/TtZMndX"), allowSendingWithoutReply: true, cancellationToken: ct);
                }
                else if (messageText == "Управлінський облік🐍")
                {
                    instagramMessageSentCount++;
                    await bot.SendPhotoAsync(update.Message.Chat.Id, InputFile.FromString("https://imgur.com/5eEglRY"), allowSendingWithoutReply: true, cancellationToken: ct);
                }

            }


            var currentKeyboard = _keyboardStateManager.GetCurrentKeyboard();
            if (messageText == "Наші послуги🐣")
            {
                _keyboardStateManager.ShowSubmenu();
                currentKeyboard = _keyboardStateManager.GetCurrentKeyboard();
            }
            else if (messageText == "Назад⏎")
            {
                _keyboardStateManager.HideSubmenu();
                currentKeyboard = _keyboardStateManager.GetCurrentKeyboard();
                instagramMessageSentCount++;
            }
            Message sentMessaage = await bot.SendTextMessageAsync(
            chatId: chatId,
            text: "Оберіть що вас цікавить",
            replyMarkup: currentKeyboard,
            cancellationToken: ct);

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            if (instagramMessageSentCount < 1)
            {
                
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
                instagramMessageSentCount++;
            }
            else if(instagramMessageSentCount == 4) {
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
                instagramMessageSentCount++;
            }


        }



    }
}




