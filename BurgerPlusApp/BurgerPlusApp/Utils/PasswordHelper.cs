namespace BurgerPlusApp.Utils
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            // Для простоты возвращаем пароль как есть
            // (в реальном проекте здесь было бы хеширование)
            return password;
        }
    }
}