using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Groups
{
    /// <summary>
    /// Lógica de interacción para AddLastWeigth.xaml
    /// </summary>
    public partial class AddLastWeigth : UserControl
    {
        private PigGroups _pigGroup;

        public event EventHandler<GroupsEventArgs> LastWeigthAdded;
        public AddLastWeigth(PigGroups pigGroup)
        {
            _pigGroup = pigGroup;
            InitializeComponent();
        }

        public virtual void OnLAstWeigthAdded(PigGroups group)
        {
            LastWeigthAdded?.Invoke(this, new GroupsEventArgs { Group = group });
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            _pigGroup.lastWeigth_avg = Double.Parse(TextBox.Text);
            OnLAstWeigthAdded(_pigGroup);
        }
    }
}
