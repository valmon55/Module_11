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
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _audioFileHandler = audioFileHandler;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            //Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;
            
            await _audioFileHandler.Download(fileId,ct);
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Голосовое сообщение загружено", cancellationToken: ct);

            string userLanguageCode = _memoryStorage.GetSession(message.Chat.Id).LanguageCode;
            _audioFileHandler.Process(userLanguageCode);
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообщение конвертировано в формат .WAV", cancellationToken: ct);
        }
    }
}
