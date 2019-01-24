using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Monitor_Plugin.Inventor_API;
using Monitor_Plugin.Parameters;

namespace Monitor_Plugin
{
    public partial class MainForm : Form
    {
        private MonitorParameters _monitorParameters;
        private MonitorManager _monitorManager;
        private InventorAPI _inventorAPI;
        private Dictionary<PluginReporter.TypeError,
            TextBox> _typeErrorToTextBoxDictionary;

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
        private void ShowError()
        {
            //_typeErrorToTextBoxDictionary[PluginReporter.Instance().
            //    LastAddedError].BackColor = Color.LightSalmon;

            MessageBox.Show(PluginReporter.Instance().GetLastError(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

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
                Convert.ToDouble(StandHeightTextBox.Text), //15,
                Convert.ToDouble(StandDiameterTextBox.Text), //200,
                Convert.ToDouble(LegHeightTextBox.Text), //50,
                Convert.ToDouble(LegWidthTextBox.Text), //60,
                Convert.ToDouble(LegThiknessTextBox.Text), //20,
                Convert.ToDouble(ScreenHeightTextBox.Text), //330,
                Convert.ToDouble(ScreenWidthTextBox.Text), //550,
                Convert.ToDouble(ScreenThiknessTextBox.Text), //30
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
            _monitorManager.CreateMonitor();
        }

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

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.BackColor = Color.White;
        }

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

        private void ClearButton_Click(object sender, EventArgs e)
        {
            foreach (var i in _typeErrorToTextBoxDictionary.Values)
            {
                i.Clear();
            }
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