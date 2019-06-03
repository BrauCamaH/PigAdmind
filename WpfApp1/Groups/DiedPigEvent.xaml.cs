using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Groups
{
    /// <summary>
    /// Lógica de interacción para DiedPigEvent.xaml
    /// </summary>
    public partial class DiedPigEvent : UserControl
    {

        private PigGroups _pigGroup;

        public event EventHandler<GroupsEventArgs> PigDied;

        public DiedPigEvent(PigGroups pigGroup)
        {
            _pigGroup = pigGroup;
            InitializeComponent();
        }

        public virtual void OnPiegDied(PigGroups group)
        {
            PigDied?.Invoke(this, new GroupsEventArgs { Group = group });
        }

        void AddNumber(TextBox textBox)
        {
            int currentN = int.Parse(textBox.Text);
            currentN++;

            textBox.Text = currentN.ToString();
        }

        void SubtractNumber(TextBox textBox)
        {
            int currentN = int.Parse(textBox.Text);
            if (currentN > 1)
            {
                currentN--;
                textBox.Text = currentN.ToString();
            }
        }
        private void Minus_OnClick(object sender, RoutedEventArgs e)
        {
            SubtractNumber(NPigsTextBox);
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            AddNumber(NPigsTextBox);
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            var group = unitOfWork.Groups.Get(_pigGroup.id);

            int diedPigs = int.Parse(NPigsTextBox.Text);
            group.died_pigs += diedPigs;
            group.pig_count -= diedPigs;

            unitOfWork.Complete();

            OnPiegDied(group);
        }
    }
}
