using System.Linq;
using System.Windows.Forms;

namespace Monitor_Plugin
{
    /// <summary>
    /// Parameter Validation Class
    /// </summary>
    internal class ValueParameterChecking
    {
        /// <summary>
        /// Button code 'Backspace' - 8
        /// </summary>
        private const int ButtonBackspace = 8;

        /// <summary>
        /// Restriction of keystrokes, except numbers and 'Backspace'
        /// </summary>
        /// <param name="keyPress"> Key pressed </param>
        public static void CheckOnlyNumbers(KeyPressEventArgs keyPress)
        {
            if (!((keyPress.KeyChar >= '0') && (keyPress.KeyChar <= '9')
                  || (keyPress.KeyChar == ButtonBackspace)))
            {
                keyPress.KeyChar = '\0';
            }
        }

        /// <summary>
        /// Limit the input of more N char
        /// </summary>
        /// <param name="textBox"> The field in which to enter </param>
        /// <param name="keyPress"> Key pressed </param>
        /// <param name="countChar"> Maximum number of characters to enter </param>
        public static void CheckNoMoreNChar(TextBox textBox,
	        KeyPressEventArgs keyPress, int countChar)
        {
            if ((textBox.Text != string.Empty) &&
                (keyPress.KeyChar != ButtonBackspace))
            {
                if (textBox.Text.Count() >= (countChar))
                {
                    keyPress.KeyChar = '\0';
                }
            }
        }
    }
}