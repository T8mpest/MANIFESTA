using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace MANIFESTA
{
    internal class MessageProcessor
    {
        private readonly ITelegramBotClient _botClient;
        private readonly Message _message;

        public MessageProcessor(ITelegramBotClient botClient, Message message)
        {
            _botClient = botClient;
            _message = message;
        }

        public async Task ProcessMessage()
        {
            string responseMessage = "";

            if (_message.Text.Contains("services"))
            {
                responseMessage = "We provide legal and accounting services.";
            }
            // Додайте інші обробники для різних команд тут

            else
            {
                responseMessage = "Hello! How can I assist you? You can ask about our services or departments.";
            }

            await _botClient.SendTextMessageAsync(_message.Chat.Id, responseMessage);
        }
    }
}
