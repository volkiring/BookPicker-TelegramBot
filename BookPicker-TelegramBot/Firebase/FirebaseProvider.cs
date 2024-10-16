using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot.Firebase
{
    public class FirebaseProvider
    {
        private const string BasePath = "https://bookpicker-telegram-bot-default-rtdb.firebaseio.com";
        private const string Secret = "wOQSVgy8dr7VDXOPz1EacfayZOaiYoaEt4TvTFRc";

        private readonly FirebaseClient client;

        public FirebaseProvider()
        {
            client = new FirebaseClient(BasePath, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(Secret)
            });

        }

        public async Task<T> TryGetAsync<T>(string key)
        {
            return await client.Child(key).OnceSingleAsync<T>();
        }

        public async Task AddOrUpdateAsync<T>(string key, T item)
        {
            await client.Child(key).PutAsync(item);
        }
    }
}
