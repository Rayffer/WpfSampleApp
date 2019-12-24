using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfSampleApp
{
    public class NumericTextBox :TextBox
    {
        public NumericTextBox()
        {
            this.PreviewTextInput += NumericTextbox_PreviewTextInput;
        }

        ~NumericTextBox()
        {
            this.PreviewTextInput -= NumericTextbox_PreviewTextInput;
        }

        private void NumericTextbox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int tempValue))
            {
                e.Handled = true;
            }
        }
    }
}
