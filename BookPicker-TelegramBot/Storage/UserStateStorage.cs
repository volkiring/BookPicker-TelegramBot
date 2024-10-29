﻿using BookPicker_TelegramBot.User;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPicker_TelegramBot.Storage
{
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text.Json;

    public class UserStateStorage
    {
        private readonly ConcurrentDictionary<long, UserState> cache = new ConcurrentDictionary<long, UserState>();
        private const string FilePath = "userStates.json";

        public UserStateStorage()
        {
            LoadFromFile(); 
        }

        public void AddOrUpdate(long telegramId, UserState userState)
        {
            cache.AddOrUpdate(telegramId, userState, (x, y) => userState);
            SaveToFile(); 
        }

        public bool TryGet(long telegramId, out UserState userState)
        {
            return cache.TryGetValue(telegramId, out userState);
        }

        private void SaveToFile()
        {
            try
            {
                var json = JsonSerializer.Serialize(cache);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user states to file: {ex.Message}");
            }
        }

        private void LoadFromFile()
        {
            if (!File.Exists(FilePath)) return;

            try
            {
                var json = File.ReadAllText(FilePath);
                var data = JsonSerializer.Deserialize<ConcurrentDictionary<long, UserState>>(json);
                if (data != null)
                {
                    foreach (var kvp in data)
                    {
                        cache[kvp.Key] = kvp.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user states from file: {ex.Message}");
            }
        }
    }

}
