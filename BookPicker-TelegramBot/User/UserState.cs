using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookPicker_TelegramBot.User.Pages;

namespace BookPicker_TelegramBot.User
{
    public record class UserState(Stack<IPage> Pages, UserData UserData)
    {
        public IPage CurrentPage => Pages.Peek();

        public void AddPage(IPage page)
        {
            if (CurrentPage.GetType() != page.GetType())
            {
                Pages.Push(page);
            }
        }

    }

}
