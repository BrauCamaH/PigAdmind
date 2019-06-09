using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1.CustomUserControls
{
    /// <summary>
    /// Interaction logic for JustNumbersTextBox.xaml
    /// </summary>
    public partial class JustNumbersTextBox : TextBox
    {
        public JustNumbersTextBox()
        {
            InitializeComponent();
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
