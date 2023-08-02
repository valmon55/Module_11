using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using CS_Basic.Telegram_Bot.Configuration;
using CS_Basic.Telegram_Bot.Services;

namespace CS_Basic.Telegram_Bot.Controllers
{
    public class VoiceMessageController
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(AppSettings appSettings, ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler)
        {
            _appSettings = appSettings;
            _telegramClient = telegramBotClient;
            _audioFileHandler = audioFileHandler;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            //Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;

            await _audioFileHandler.Download(fileId,ct);
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Голосовое сообщение загружено", cancellationToken: ct);
        }
    }
}
