using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Groups
{
    /// <summary>
    /// Interaction logic for EditGroup.xaml
    /// </summary>
    public partial class EditGroup : UserControl
    {
        private PigGroups _pigGroup;

        public event EventHandler<GroupsEventArgs> GroupEdited;
        public EditGroup()
        {
            InitializeComponent();
        }
        public virtual void OnGroupEdited(PigGroups group)
        {
            GroupEdited?.Invoke(this, new GroupsEventArgs { Group = group });
        }
        public EditGroup(PigGroups pigGroup)
        {
            InitializeComponent();
            _pigGroup = pigGroup;
            UserAgree.AcceptButton = Accept_btn;

            GetGroup();
        }
        private void EditGroupFromDatabase(string newId, string newNumber, string newWeigth, string newDate)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var group = unitOfWork.Groups.Get(_pigGroup.id);
            group.name = newId;
            group.pig_count = int.Parse(newNumber);
            group.weigth_avg = double.Parse(newWeigth);
            group.weaning_date = newDate;
            unitOfWork.Complete();
            OnGroupEdited(group);

        }

        private void GetGroup()
        {
            NameTextBox.Text = _pigGroup.name;
            NumberTexBox.Text = _pigGroup.pig_count.ToString();
            WeigthTexBox.Text = _pigGroup.weigth_avg.ToString();
            DatePicker.Text = _pigGroup.weaning_date;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Accept_btn_OnClick(object sender, RoutedEventArgs e)
        {
            EditGroupFromDatabase(NameTextBox.Text, NumberTexBox.Text, WeigthTexBox.Text, DatePicker.Text);
        }
    }
}
