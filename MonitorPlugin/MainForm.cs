using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Monitor_Plugin.Inventor_API;
using Monitor_Plugin.Parameters;

namespace Monitor_Plugin
{
    /// <summary>
    /// Main form class
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Monitor parameters
        /// </summary>
        private MonitorParameters _monitorParameters;

        /// <summary>
        /// Monitor manager
        /// </summary>
        private MonitorManager _monitorManager;

        /// <summary>
        /// Inventor API
        /// </summary>
        private InventorAPI _inventorAPI;

        /// <summary>
        /// Dictionary of errors
        /// </summary>
        private Dictionary<PluginReporter.TypeError,
            TextBox> _typeErrorToTextBoxDictionary;

        /// <summary>
        /// Constructor class
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            PluginReporter.Instance().OnAdd += ShowError;

            _typeErrorToTextBoxDictionary =
                new Dictionary<PluginReporter.TypeError,
                    TextBox>()
                {
                    { PluginReporter.TypeError.ErrorStandHeight, StandHeightTextBox},
                    { PluginReporter.TypeError.ErrorStandDiameter, StandDiameterTextBox},
                    { PluginReporter.TypeError.ErrorLegHeight, LegHeightTextBox},
                    { PluginReporter.TypeError.ErrorLegWidth, LegWidthTextBox},
                    { PluginReporter.TypeError.ErrorLegThikness, LegThiknessTextBox},
                    { PluginReporter.TypeError.ErrorScreenHeight, ScreenHeightTextBox},
                    { PluginReporter.TypeError.ErrorScreenWidth, ScreenWidthTextBox},
                    { PluginReporter.TypeError.ErrorScreenThikness, ScreenThiknessTextBox},
                };
        }

        /// <summary>
        /// Error display method
        /// </summary>
        private void ShowError()
        {
            MessageBox.Show(PluginReporter.Instance().GetLastError(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Actions when form is loaded
        /// </summary>
        /// <param name="sender">Some sender</param>
        /// <param name="e">Some event</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var i in _typeErrorToTextBoxDictionary.Values)
            {
                i.BackColor = Color.White;
            }
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            if (CheckCellsEmpty())
            {
                return;
            }

            List<double> listMonitorParameters = new List<double>()
            {
                Convert.ToDouble(StandHeightTextBox.Text),
                Convert.ToDouble(StandDiameterTextBox.Text),
                Convert.ToDouble(LegHeightTextBox.Text),
                Convert.ToDouble(LegWidthTextBox.Text),
                Convert.ToDouble(LegThiknessTextBox.Text),
                Convert.ToDouble(ScreenHeightTextBox.Text),
                Convert.ToDouble(ScreenWidthTextBox.Text),
                Convert.ToDouble(ScreenThiknessTextBox.Text),
            };

            _monitorParameters = new MonitorParameters(listMonitorParameters);
            if (_monitorParameters.StandParam == null ||
                _monitorParameters.LegParam == null ||
                 _monitorParameters.ScreenParam == null)
            {
                return;
            }

            _inventorAPI = new InventorAPI();
            _monitorManager = new MonitorManager(_inventorAPI, _monitorParameters);
            _monitorManager.CreateMonitor(BackCheckBox.Checked);
        }

        /// <summary>
        /// Check cells for empties
        /// </summary>
        /// <returns>Empty cells flag</returns>
        private bool CheckCellsEmpty()
        {
            bool cellsNotEmpty = false;

            foreach (var i in _typeErrorToTextBoxDictionary.Values)
            {
                if (i.Text == string.Empty)
                {
                    i.BackColor = Color.LightSalmon;
                    cellsNotEmpty = true;
                }
            }
            return cellsNotEmpty;
        }

        /// <summary>
        /// Clear Textbox BackColor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.BackColor = Color.White;
        }


        /// <summary>
        /// TextBox Keyress event validate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            ValueParameterChecking.CheckOnlyNumbers(e);

            if (textBox.Name == "ScreenWidthTextBox")
            {
                ValueParameterChecking.CheckNoMoreNChar(textBox, e, 4);
            }
            else if (textBox.Name == "ScreenThiknessTextBox" || textBox.Name == "LegHeightTextBox" ||
                     textBox.Name == "LegThiknessTextBox" || textBox.Name == "StandHeightTextBox")
            {
                ValueParameterChecking.CheckNoMoreNChar(textBox, e, 2);
            }
            else
            {
                ValueParameterChecking.CheckNoMoreNChar(textBox, e, 3);
            }
        }

        /// <summary>
        /// Clear all fields and checkboxes button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            foreach (var i in _typeErrorToTextBoxDictionary.Values)
            {
                i.Clear();
            }

            BackCheckBox.Checked = false;
        }

        #region TextBox TextChanged Events

        private void ScreenHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox_TextChanged(sender, e);
        }

        private void ScreenWidthTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox_TextChanged(sender, e);
        }

        private void ScreenThiknessTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox_TextChanged(sender, e);
        }

        private void LegHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox_TextChanged(sender, e);
        }

        private void LegWidthTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox_TextChanged(sender, e);
        }

        private void LegThiknessTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox_TextChanged(sender, e);
        }

        private void StandHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox_TextChanged(sender, e);
        }

        private void StandDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox_TextChanged(sender, e);
        }

        #endregion

        #region TextBox KeyPress Events

        private void ScreenHeightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(sender, e);
        }

        private void ScreenWidthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(sender, e);
        }

        private void ScreenThiknessTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(sender, e);
        }

        private void LegHeightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(sender, e);
        }

        private void LegWidthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(sender, e);
        }

        private void LegThiknessTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(sender, e);
        }

        private void StandHeightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(sender, e);
        }

        private void StandDiameterTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(sender, e);
        }

        #endregion
    }
}