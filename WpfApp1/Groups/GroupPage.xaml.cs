using System.Windows.Controls;
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
    }
}


