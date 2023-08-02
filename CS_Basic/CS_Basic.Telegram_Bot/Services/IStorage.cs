using CS_Basic.Telegram_Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Basic.Telegram_Bot.Services
{
    public interface IStorage
    {
        /// <summary>
        /// Получение сессии пользователя по идентификатору
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        Session GetSession(long chatId);
    }
}
