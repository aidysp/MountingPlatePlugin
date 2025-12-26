using System.Linq;

namespace MountingPlatePlugin.View
{
    public static class Validator
    {
        /// <summary>
        /// Проверка вводимых символов в TextBox (только цифры и точка).
        /// </summary>
        public static string TextBoxCheck(string value)
        {
            const string allowedChars = ".1234567890";
            return new string(value.Where(character => 
                allowedChars.Contains(character)).ToArray());
        }
        
        /// <summary>
        /// Проверяет, является ли строка валидным числом float.
        /// </summary>
        public static bool IsValidFloat(string text, out float result)
        {
            return float.TryParse(text, out result);
        }
        
        /// <summary>
        /// Проверяет, является ли строка валидным целым числом.
        /// </summary>
        public static bool IsValidInt(string text, out int result)
        {
            return int.TryParse(text, out result);
        }
    }
}
