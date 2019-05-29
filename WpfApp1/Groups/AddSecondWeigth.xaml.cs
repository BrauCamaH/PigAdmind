using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Groups
{
    /// <summary>
    /// Lógica de interacción para AddSecondWeigth.xaml
    /// </summary>
    public partial class AddSecondWeigth : UserControl
    {
        private PigGroups _pigGroup;

        public event EventHandler<GroupsEventArgs> SecondWeigthAdded;
        public AddSecondWeigth(PigGroups pigGroup)
        {
            _pigGroup = pigGroup;
            InitializeComponent();
        }
        public virtual void OnSecondWeigthAdded(PigGroups group)
        {
            SecondWeigthAdded?.Invoke(this, new GroupsEventArgs { Group = group });
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            _pigGroup.second_avg = Double.Parse(TextBox.Text);
            OnSecondWeigthAdded(_pigGroup);
        }
    }
}
