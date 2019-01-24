using System.Linq;
using System.Windows.Forms;

namespace Monitor_Plugin
{
    internal class ValueParameterChecking
    {
        private const int ButtonBackspace = 8;

        public static void CheckOnlyNumbers(KeyPressEventArgs keyPress)
        {
            if (!((keyPress.KeyChar >= '0') && (keyPress.KeyChar <= '9')
                  || (keyPress.KeyChar == ButtonBackspace)))
            {
                keyPress.KeyChar = '\0';
            }
        }

        public static void CheckNoMoreNChar(TextBox textBox, KeyPressEventArgs keyPress, int countChar)
        {
            if ((textBox.Text != string.Empty) && (keyPress.KeyChar != ButtonBackspace))
            {
                if (textBox.Text.Count() >= (countChar))
                {
                    keyPress.KeyChar = '\0';
                }
            }
        }
    }
}