using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;

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
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("6624725666:AAFPeHrqbTi2APohoKj5Omm_kzcWxDqTUJ0"));
            services.AddHostedService<Bot>();
        }
    }
}