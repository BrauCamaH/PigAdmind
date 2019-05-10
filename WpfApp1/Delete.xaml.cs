using System.Windows;
using System.Windows.Controls;
using WpfApp1.Interfaces;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Delete.xaml
    /// </summary>
    public partial class Delete : UserControl
    {
        private IRemovable _iRemovableItem;

        public Delete()
        {
            InitializeComponent();
        }

        public Delete(IRemovable iRemovableItem)
        {
            InitializeComponent();
            _iRemovableItem = iRemovableItem;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Accept_btn.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Accept_btn.IsEnabled = false;
        }

        private void Accept_Btn_Click(object sender, RoutedEventArgs e)
        {
            CheckBox.IsChecked = false;
            if (_iRemovableItem != null)
            {
                _iRemovableItem.RemoveSelectedItem();
            }
        }
    }
}
