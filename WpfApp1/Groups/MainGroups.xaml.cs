using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.CustomUserControls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Managers;
using WpfApp1.Persistance;

namespace WpfApp1.Groups
{
    /// <summary>
    /// Interaction logic for MainGroups.xaml
    /// </summary>
    public partial class MainGroups : UserControl, IPigAdmindPage<PigGroups, GroupsEventArgs>
    {
        private PigGroups CurrenGroup { get; set; }
        private ObservableCollection<PigGroups> _groupsObservableList;

        private BackButton _backButton;
        private EditAndDelete _editAndDelete;

        private DeleteGroup _deleteGroup;
        private EditGroup _editGroup;

        public MainGroups()
        {
            InitializeComponent();
        }
        public MainGroups(BackButton backButton, EditAndDelete editAndDelete)
        {
            InitializeComponent();
            _groupsObservableList = new ObservableCollection<PigGroups>();
            GetGroupsFromDataBase();

            _backButton = backButton;
            _editAndDelete = editAndDelete;

            SearchTextBox.SetView(GroupList, CustomFilter);
        }

        public void InitializeCrudControls(PigGroups entity)
        {
            _deleteGroup = new DeleteGroup(entity);
            _editGroup = new EditGroup(entity);

            _editAndDelete.DeleteControl = _deleteGroup;
            _editAndDelete.EditControl = _editGroup;

            _deleteGroup.GroupDeleted += OnItemDeleted;
            _editGroup.GroupEdited += OnItemEdited;

        }


        public bool CustomFilter(object obj)
        {
            if (String.IsNullOrEmpty(SearchTextBox.TextBox.Text))
                return true;
            else
            {
                return (((DatabaseFirst.PigGroups)obj).name.IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.PigGroups)obj).pig_count.ToString().IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.PigGroups)obj).weigth_avg.ToString().IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.PigGroups)obj).weaning_date.IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        public void GetItemsFromDatabase()
        {

        }

        public void OnItemAdded(object sender, GroupsEventArgs e)
        {

        }

        public void OnItemDeleted(object sender, GroupsEventArgs e)
        {
            _groupsObservableList.Remove(_groupsObservableList.Single(i => i.id == CurrenGroup.id));
            // MessageBox.Show("Group Deleted");
        }

        public void OnItemEdited(object sender, GroupsEventArgs e)
        {
            _groupsObservableList[_groupsObservableList.IndexOf(CurrenGroup)] = e.Group;
        }


        private bool IsDataComplete(string txtbox, string datePicker)
        {
            if (txtbox != "" && datePicker != "")
            {
                return true;
            }
            return false;
        }
        private void GetGroupsFromDataBase()
        {
            var unitOfWork = new Entities();
            foreach (var pigGroups in unitOfWork.PigGroups.ToList())
            {
                if (_groupsObservableList.Count < unitOfWork.PigGroups.ToList().Count)
                {
                    _groupsObservableList.Add(pigGroups);
                }
            }
            GroupList.ItemsSource = _groupsObservableList;
        }

        private void AddNewGroup(string id, float weigth, int n, string date)
        {
            var ctx = new Entities();
            var pigGroup = new DatabaseFirst.PigGroups()
            {
                weaning_date = date,
                weigth_avg = weigth,
                second_avg = 0,
                lastWeigth_avg = 0,
                pig_count = n,
                died_pigs = 0,
                user = 1,
                name = id
            };
            _groupsObservableList.Add(pigGroup);
            ctx.PigGroups.Add(pigGroup);
            ctx.SaveChanges();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _editAndDelete.IsEnabled = GroupList.SelectedItem != null;
            PigGroups group = (PigGroups)GroupList.SelectedItem;
            CurrenGroup = group;

            var unitOfWork = new UnitOfWork(new Entities());
            if (group != null) InitializeCrudControls(unitOfWork.Groups.Get(group.id));
        }
        private void OnListMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            MainGridManager.SetUserControl(new GroupPage());
        }

        private void ClearFields()
        {
            NPigsBox.Text = "";
            WeigthAVGBox.Text = "";
            DatePicker.Text = "";
            IdBox.Text = "";
        }

        private void AddNewGroup_OnClick(object sender, RoutedEventArgs e)
        {
            int nPigs = Int32.Parse(NPigsBox.Text);
            float weigth = float.Parse(WeigthAVGBox.Text);
            string weanigDate = DatePicker.Text;
            string id = IdBox.Text;
            if (IsDataComplete(NPigsBox.Text, weanigDate))
            {
                AddNewGroup(id, weigth, nPigs, weanigDate);
                ClearFields();
            }

        }

    }
}
