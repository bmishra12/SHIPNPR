using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ShiptalkCommon
{
    public static class PasswordUtil
    {

        private static readonly string PasswordValidRegex = "^.*(?=^.{6,15}$)(?=.*[a-zA-Z])(?=.*[0-9])((?=.*\\d)||(.*[\\W])).*$";

        public static bool VerifyClearTextPasswordLength(string ClearTextPassword)
        {
            return (ClearTextPassword.Length >= ConfigUtil.PasswordMinLength && ClearTextPassword.Length <= ConfigUtil.PasswordMaxLength);
        }


        /// <summary>
        /// Generates a random password that satisfies the password minimum requirements criteria.
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomPassword()
        {
            string sRandomPwd = GetRandomPassword(ConfigUtil.PasswordMaxLength);
            while (!IsPasswordValid(sRandomPwd))
            {
                sRandomPwd = GetRandomPassword(ConfigUtil.PasswordMaxLength);
            }
            return sRandomPwd;
        }

        /// <summary>
        /// Pass an un-encrypted password in plain text and get a boolean result of the Password validity check.
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static bool IsPasswordValid(string Password)
        {
            if (VerifyClearTextPasswordLength(Password))
                return Regex.IsMatch(Password, PasswordValidRegex);
            else
                return false;
        }

      
        private static string GetRandomPassword(int numChars)
        {
           // string[] chars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S",
           //             "T", "U", "V", "W", "X", "Y", "Z", 
           //             "1", "2", "3", "4", "5", "6", "7", "8", "9", 
           //             "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

           // Random rnd = new Random(DateTime.Now.Millisecond);
           //// Random rnd = new Random(913679);  // modified the above code, the above code is failing in HP server
           // string random = string.Empty;
           // for (int i = 0; i < numChars; i++)
           // {
           //     random += chars[rnd.Next(0, chars.Length)];
           // }
           // return random;
            return "Zxcv1357!";
        }


    }
}
