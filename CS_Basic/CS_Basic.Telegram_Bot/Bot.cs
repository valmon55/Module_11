﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Microsoft.Extensions.Hosting;

namespace CS_Basic.Telegram_Bot
{
    internal class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramClient;
        public Bot(ITelegramBotClient telegramClient)
        { 
            _telegramClient = telegramClient; 
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramClient.StartReceiving(
                HandleUpdateAsync, 
                HandleErrorAsync, 
                new ReceiverOptions() { AllowedUpdates = { } }, 
                cancellationToken: stoppingToken);  

            Console.WriteLine("Бот запущен!");
        }
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if(update.Type == UpdateType.CallbackQuery)
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы нажали кнопку",cancellationToken : cancellationToken);
                return;
            }
            if (update.Type == UpdateType.Message)
            {
                Console.WriteLine($"Получено сообщение {update.Message.Text}");
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, $"Вы отправили сообщение {update.Message.Text}", cancellationToken: cancellationToken); ; ; ; ; ;
                return;
            }

        }
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(errorMessage);
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }
    }
}
