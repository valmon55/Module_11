﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Basic.Telegram_Bot.Extensions
{
    public static class DirectoryExtension
    {
        public static string GetSolutionRoot()
        {
            var dir = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            var fullname = Directory.GetParent(dir).FullName;
            var projectRoot = fullname.Substring(0, fullname.Length - 4);
            return Directory.GetParent(projectRoot)?.FullName;
        }
    }
}
