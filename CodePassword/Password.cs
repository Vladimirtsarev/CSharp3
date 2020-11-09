using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePassword
{
    public static class Password
    {
        /// <summary>
        /// Дешифрует пароль
        /// </summary>
        /// <param name="p_sPassw"></param>
        /// <returns>расшифрованый</returns>
        public static string getPassword(string p_sPassw)
        {
            string password = "";
            foreach (char a in p_sPassw)
            {
                char ch = a;
                ch--;
                password += ch;
            }
            return password;
        }

        /// <summary>
        /// Шифрует пароль
        /// </summary>
        /// <param name="p_sPassword"></param>
        /// <returns>зашифрованый</returns>
        public static string getCodPassword(string p_sPassword)
        {
            string sCode = "";
            foreach (char a in p_sPassword)
            {
                char ch = a;
                ch++;
                sCode += ch;
            }
            return sCode;
        }
    }
}
