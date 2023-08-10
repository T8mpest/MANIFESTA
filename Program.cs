﻿using Telegram.Bot;
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
                if (messageText == "Юр. послуги")
                {
                    // HTML-formatted text with a link to the image
                    var legalServicesText = "<b>Юридические услуги</b>\n" +
                                            "<i>Предоставляем широкий спектр юридических услуг...</i>\n" +
                                            "<a href=\"https://example.com\">Подробнее</a>";

                    // Send the formatted text message with HTML parse mode
                    await bot.SendTextMessageAsync(update.Message.Chat.Id, legalServicesText, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html, cancellationToken: ct);

                    // Send the image as a separate message
                    var imageUrl = "https://example.com/image.jpg"; // Replace with the actual image URL
                    await bot.SendPhotoAsync(
    chatId: chatId,
    photo: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg"),
    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
    parseMode: ParseMode.Html,
    cancellationToken: ct);
                }
            }

           
            var currentKeyboard = _keyboardStateManager.GetCurrentKeyboard();
            if (messageText == "Наші послуги")
            {
                _keyboardStateManager.ShowSubmenu();
                currentKeyboard = _keyboardStateManager.GetCurrentKeyboard();
            }
            else if (messageText == "Назад")
            {
                _keyboardStateManager.HideSubmenu();
                currentKeyboard = _keyboardStateManager.GetCurrentKeyboard();
            }
            Message sentMessaage = await bot.SendTextMessageAsync(
            chatId: chatId,
            text: "Оберіть що вас цікавить",
            replyMarkup: currentKeyboard,
            cancellationToken: ct);

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

            //ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            //{
            //   // new KeyboardButton("Наші послуги"),
            //   // new KeyboardButton("Прайси"),
            //    KeyboardButton.WithRequestContact("Залишити заявку☎️"),
            //});

            //Message ssentMessage = await bot.SendTextMessageAsync(
            //    chatId: chatId,
            //    text: "Оберіть що вас цікавить",
            //    replyMarkup: replyKeyboardMarkup,
            //    cancellationToken: ct);
            //Console.WriteLine(
            //$"{message.From.FirstName} sent message {message.MessageId} " +
            //$"to chat {message.Chat.Id} at {message.Date.ToLocalTime}. " +
            //    $"It is a reply to message {message.ReplyToMessage?.MessageId} " +
            //    $"and has {message.Entities?.Length ?? 0} message entities.");

        }



    }


}




