using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using BurgerPlusApp.Models;

namespace BurgerPlusApp.Utils
{
    public static class FileHelper
    {
        private static string ПутьКФайлу = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "users.json"
        );

        public static void Сохранить(List<User> пользователи)
        {
            try
            {
                string json = JsonConvert.SerializeObject(пользователи, Formatting.Indented);
                File.WriteAllText(ПутьКФайлу, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка сохранения: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public static List<User> Загрузить()
        {
            try
            {
                if (!File.Exists(ПутьКФайлу))
                {
                    return ТестовыеПользователи();
                }

                string json = File.ReadAllText(ПутьКФайлу);
                var пользователи = JsonConvert.DeserializeObject<List<User>>(json);

                if (пользователи == null || пользователи.Count == 0)
                {
                    return ТестовыеПользователи();
                }

                return пользователи;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка загрузки: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return ТестовыеПользователи();
            }
        }

        private static List<User> ТестовыеПользователи()
        {
            return new List<User>
            {
                new User { Id = 1, Login = "admin", Password = "admin", Role = "admin", IsBlocked = false },
                new User { Id = 2, Login = "user1", Password = "user1", Role = "user", IsBlocked = false },
                new User { Id = 3, Login = "user2", Password = "user2", Role = "user", IsBlocked = true }
            };
        }
    }
}