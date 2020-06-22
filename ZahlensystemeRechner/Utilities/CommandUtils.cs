using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner.Utilities
{
    class CommandUtils
    {

        /// <summary>
        /// Überprüft ob die eingabe ein befehl ist
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsInputACommand(string input)
        {
            //ein Befehl fängt mit einem / an
            if (input[0] != '/')
            {
                return false;
            }

            return false;
        }
    }
}
