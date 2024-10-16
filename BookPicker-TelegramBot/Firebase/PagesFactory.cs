using BookPicker_TelegramBot.User.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot.Firebase
{
    public class PagesFactory
    {
        public static IPage GetPage(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes().FirstOrDefault(t => t.Name == name && typeof(IPage).IsAssignableFrom(t));

            return (IPage)Activator.CreateInstance(type);
        }
    }
}
