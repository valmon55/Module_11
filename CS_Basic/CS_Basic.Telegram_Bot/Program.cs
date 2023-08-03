using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using CS_Basic.Telegram_Bot.Controllers;
using CS_Basic.Telegram_Bot.Services;
using CS_Basic.Telegram_Bot.Configuration;

namespace CS_Basic.Telegram_Bot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();
            
            Console.WriteLine("Сервис запущен.");
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен.");
        }
        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddSingleton<IFileHandler, AudioFileHandler>();
            services.AddSingleton<IStorage, MemoryStorage>();

            services.AddTransient<DefaultMessageController> ();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();
        }
        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                DownloadsFolder = "D:\\TelegramBot\\Download",
                BotToken = "6624725666:AAFPeHrqbTi2APohoKj5Omm_kzcWxDqTUJ0",
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
                OutputAudioFormat = "wav",
                InputAudioBitrate = 16000,
            };
        }
    }
}