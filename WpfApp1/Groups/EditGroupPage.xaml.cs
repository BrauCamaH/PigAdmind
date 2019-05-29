using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Groups
{
    /// <summary>
    /// Lógica de interacción para EditGroupPage.xaml
    /// </summary>
    public partial class EditGroupPage : UserControl
    {
        private PigGroups _pigGroup;

        public event EventHandler<GroupsEventArgs> GroupEdited;
        public EditGroupPage()
        {
            InitializeComponent();
        }

        public EditGroupPage(PigGroups pigGroup)
        {
            InitializeComponent();
            _pigGroup = pigGroup;
            UserAgree.AcceptButton = Accept_btn;

            GetGroup();
        }
        public virtual void OnGroupEdited(PigGroups group)
        {
            GroupEdited?.Invoke(this, new GroupsEventArgs { Group = group });
        }
        private void EditGroupFromDatabase(double secondAVG, double lastAVG, int diedPigs)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var group = unitOfWork.Groups.Get(_pigGroup.id);
            group.second_avg = secondAVG;
            group.lastWeigth_avg = lastAVG;
            group.died_pigs = diedPigs;
            unitOfWork.Complete();
            OnGroupEdited(group);
        }

        private void GetGroup()
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var group = unitOfWork.Groups.GetGroupById(_pigGroup.id);
            SecondAVGTextBox.Text = group.second_avg.ToString();
            LastAVGTexBox.Text = group.lastWeigth_avg.ToString();
            DeadPigsTexBox.Text = group.died_pigs.ToString();
        }
        private void Accept_btn_OnClick(object sender, RoutedEventArgs e)
        {
            EditGroupFromDatabase(double.Parse(SecondAVGTextBox.Text), double.Parse(LastAVGTexBox.Text), int.Parse(DeadPigsTexBox.Text));
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
