using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Groups
{
    /// <summary>
    /// Interaction logic for GroupPage.xaml
    /// </summary>
    public partial class GroupPage : UserControl
    {
        private PigGroups _group;
        public GroupPage()
        {
            InitializeComponent();
        }

        public GroupPage(PigGroups group)
        {
            InitializeComponent();

            SetGroup(group);
            _group = group;
        }

        private void SetGroup(PigGroups group)
        {
            CodeLabel.Content = "Grupo: " + group.name;
            NTotalLabel.Content = "Lechones: " + group.pig_count;
            WeigthTextBlock.Text = group.weigth_avg.ToString();
            SecondTextBlock.Text = group.second_avg.ToString();
            LastTextBlock.Text = group.lastWeigth_avg.ToString();
            DiedTextBlock.Text = group.died_pigs.ToString();

        }



        private void AddUserControlToEventDialog(UserControl userControl)
        {
            FemaleEventDialog.IsOpen = true;
            MainGridEvent.Children.Clear();
            MainGridEvent.Children.Add(userControl);
        }

        private void SecondAvg_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void LastAvg_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void DiedPig_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            var edit = new EditGroupPage(_group);
            edit.GroupEdited += EditOnGroupEdited;

            AddUserControlToEventDialog(edit);
        }

        private void EditOnGroupEdited(object sender, GroupsEventArgs e)
        {
            SetGroup(e.Group);
        }
    }
}


