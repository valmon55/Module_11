﻿using CS_Basic.Telegram_Bot.Configuration;
using CS_Basic.Telegram_Bot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace CS_Basic.Telegram_Bot.Controllers
{
    internal class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient,IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            //Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;
            string languageText = callbackQuery.Data switch
            {
                "ru" => "ru Русский",
                "en" => "gb Английский",
                "fr" => "fr Французский",
                _ => string.Empty
            };


            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Язык аудио - {languageText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.",
                cancellationToken: ct,
                parseMode: ParseMode.Html);
        }
    }
}
