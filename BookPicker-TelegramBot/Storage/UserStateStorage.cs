using BookPicker_TelegramBot.User;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot.Storage
{
    using BookPicker_TelegramBot.Firebase;
    using BookPicker_TelegramBot.User.Pages;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text.Json;

    public class UserStateStorage
    {
        private readonly FirebaseProvider firebaseProvider = new();

        public void AddOrUpdate(long telegramId, UserState userState)
        {
            var userStateFirebase = ToUserStateFirebase(userState);
            firebaseProvider.AddOrUpdateAsync($"userstates/{telegramId}", userStateFirebase);
        }

        public async Task<UserState?> TryGetAsync(long telegramId)
        {
            var userStateFirebase = await firebaseProvider.TryGetAsync<UserStateFirebase>($"userstates/{telegramId}");
            if (userStateFirebase == null)
            {
                return null;
            }
            return ToUserState(userStateFirebase);
        }

        private static UserState? ToUserState(UserStateFirebase userStateFirebase)
        {
            var pages = userStateFirebase.PageNames?.Select(x => PagesFactory.GetPage(x)).Reverse().ToList();
            return new UserState(new Stack<IPage>(pages), userStateFirebase.UserData);

        }

        private static UserStateFirebase ToUserStateFirebase(UserState userState)
        {
            return new UserStateFirebase
            {
                UserData = userState.UserData,
                PageNames = userState.Pages?.Select(x => x.GetType().Name).ToList()
            };
        }

        public async Task<List<UserState>> GetAllUsersAsync()
        {
            var allUserStatesFirebase = await firebaseProvider.GetAllAsync<UserStateFirebase>("userstates");

            return allUserStatesFirebase
                .Select(ToUserState)
                .Where(userState => userState != null)
                .ToList()!;
        }


    }
}
