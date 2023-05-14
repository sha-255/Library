using System;

namespace KeeperPRO.WPFClient.Common.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Must be in the format [Last name] [First Name] [Patronymic]
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns> [Last name] [First Name Initial]. [Patronymic Initial]. (Ex: Last F. P.)</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToInitials(this string fullName)
        {
            var words = fullName.Split(' ');
            if (words.Length != 3)
                throw new ArgumentException("The full name is set in the wrong format");
            return $"{words[0]} {words[1][0]}. {words[2][0]}.";
        }
    }
}
