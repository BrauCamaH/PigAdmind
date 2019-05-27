using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.CustomUserControls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Females.BirthViews;
using WpfApp1.Managers;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for MainFemales.xaml
    /// </summary>
    public partial class MainFemales : UserControl, IPigAdmindPage<DatabaseFirst.Females, FemalesEventArgs>
    {
        private DatabaseFirst.Females _selectedFemale;
        public ObservableCollection<DatabaseFirst.Females> FemalesObservableList { get; }


        private BackButton _backButton;
        private EditAndDelete _editAndDelete;

        private EditFemale _editFemale;
        private DeleteFemale _deleteFemale;

        public MainFemales(BackButton backButton, EditAndDelete editAndDelete)
        {

            InitializeComponent();
            FemalesObservableList = new ObservableCollection<DatabaseFirst.Females>();
            GetItemsFromDatabase();

            _backButton = backButton;
            _editAndDelete = editAndDelete;

            SearchTextBox.SetView(FemalesList, CustomFilter);
        }

        public MainFemales()
        {
            InitializeComponent();
        }



        public void InitializeCrudControls(DatabaseFirst.Females entity)
        {
            _deleteFemale = new DeleteFemale(entity);

            _editAndDelete.DeleteControl = _deleteFemale;

            _deleteFemale.FemaleDeleted += OnItemDeleted;
        }

        public void GetItemsFromDatabase()
        {
            var unitOfWork = new UnitOfWork(new Entities());
            foreach (var female in unitOfWork.Females.GetAll())
            {
                if (FemalesObservableList.Count < unitOfWork.Females.GetAll().Count())
                {
                    FemalesObservableList.Add(female);
                }
            }

            FemalesList.ItemsSource = FemalesObservableList;
        }

        public void OnItemAdded(object sender, FemalesEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnItemDeleted(object sender, FemalesEventArgs e)
        {
            RemoveItemFromList(FemalesObservableList, e.Female);
        }

        public void OnItemEdited(object sender, FemalesEventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool CustomFilter(object obj)
        {
            if (String.IsNullOrEmpty(SearchTextBox.TextBox.Text))
                return true;
            else
            {
                return (((DatabaseFirst.Females)obj).code.IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Females)obj).birthday.IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Females)obj).martenity.ToString().IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Females)obj).status.IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }

        private void SetBackButtonBehaviour(BackButton backButton)
        {
            backButton.SetActualContext(this);
            backButton.IsEnabled = true;
        }
        private void RemoveItemFromList(ObservableCollection<DatabaseFirst.Females> collection, DatabaseFirst.Females currentFemale)
        {
            collection.Remove(collection.Single(i => i.code.Equals(currentFemale.code)));
        }
        private void AddNewFemale(string code, string birthday)
        {
            var female = new DatabaseFirst.Females()
            {
                code = code,
                birthday = birthday,
                status = "Normal",
                martenity = 0,
                user = 1,
                misbirths = 0

            };
            FemalesObservableList.Add(female);
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.Females.Add(female);
            unitOfWork.Complete();

        }
        private bool IsDataComplete(string txtbox, string datePicker)
        {
            if (txtbox != "" && datePicker != "")
            {
                return true;
            }
            return false;
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _editAndDelete.IsEnabled = FemalesList.SelectedItem != null;
            DatabaseFirst.Females female = (DatabaseFirst.Females)FemalesList.SelectedItem;
            _editAndDelete.EditControl = new EditBirth();
            _selectedFemale = female;

            var unitOfWork = new UnitOfWork(new Entities());
            if (female != null) InitializeCrudControls(unitOfWork.Females.GetFemaleByCode(female.code));
        }

        private void OnListMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (FemalesList.SelectedItem != null)
            {
                MainGridManager.SetUserControl(new FemalePage(_selectedFemale));
                FemalesList.SelectedItem = null;
                SetBackButtonBehaviour(_backButton);
            }

        }

        private void AddNewFemale_Click(object sender, RoutedEventArgs e)
        {
            string code = CodeBox.Text;
            string birthday = DatePicker.Text;
            if (IsDataComplete(code, birthday))
            {
                AddNewFemale(code, birthday);
                CodeBox.Text = "";
                DatePicker.Text = "";
            }

        }
    }
}
