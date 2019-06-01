using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Females
{
    /// <summary>
    /// Lógica de interacción para MisbirthEvent.xaml
    /// </summary>
    public partial class MisbirthEvent : UserControl
    {
        private DatabaseFirst.Females _female;
        public MisbirthEvent(DatabaseFirst.Females female)
        {
            _female = female;
            InitializeComponent();
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
