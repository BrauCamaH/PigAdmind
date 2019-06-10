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

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for MainFemales.xaml
    /// </summary>
    public partial class MainFemales : UserControl, IPigAdmindPage<DatabaseFirst.Females, FemalesEventArgs>
    {
        private DatabaseFirst.Females _selectedFemale;
        private ObservableCollection<DatabaseFirst.Females> _FemaleObservableCollection;


        private BackButton _backButton;
        private EditAndDelete _editAndDelete;

        private EditFemale _editFemale;
        private DeleteFemale _deleteFemale;

        public MainFemales(BackButton backButton, EditAndDelete editAndDelete)
        {

            InitializeComponent();
            _FemaleObservableCollection = new ObservableCollection<DatabaseFirst.Females>();


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
            _editFemale = new EditFemale(_selectedFemale);

            _editAndDelete.DeleteControl = _deleteFemale;
            _editAndDelete.EditControl = _editFemale;

            _deleteFemale.FemaleDeleted += OnItemDeleted;
            _editFemale.FemaleEdited += OnItemEdited;
        }

        public void GetItemsFromDatabase()
        {
            var unitOfWork = new UnitOfWork(new Entities());
            foreach (var female in unitOfWork.Females.GetAll())
            {
                if (_FemaleObservableCollection.Count < unitOfWork.Females.GetAll().Count())
                {
                    _FemaleObservableCollection.Add(female);
                }
            }

            FemalesList.ItemsSource = _FemaleObservableCollection;
        }


        public void OnItemAdded(object sender, FemalesEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnItemDeleted(object sender, FemalesEventArgs e)
        {
            RemoveItemFromList(_FemaleObservableCollection, e.Female);
        }

        public void OnItemEdited(object sender, FemalesEventArgs e)
        {
            _FemaleObservableCollection[_FemaleObservableCollection.IndexOf(_selectedFemale)] = e.Female;

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
                martenity = 100,
                user = 1,
                misbirths = 0,
                successbirths = 0
            };
            _FemaleObservableCollection.Add(female);
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
