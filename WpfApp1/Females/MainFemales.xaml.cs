using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp1.CustomUserControls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Interfaces;
using WpfApp1.Managers;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for MainFemales.xaml
    /// </summary>
    public partial class MainFemales : UserControl, IRemovable
    {
        private static DatabaseFirst.Females _selectedFemale;
        public ObservableCollection<DatabaseFirst.Females> FemalesObservableList { get; }
        private CollectionView _view;

        private BackButton _backButton;
        private EditAndDelete _editAndDelete;

        public MainFemales(BackButton backButton, EditAndDelete editAndDelete)
        {

            InitializeComponent();
            FemalesObservableList = new ObservableCollection<DatabaseFirst.Females>();
            GetFemalesFromDataBase();
            _view = (CollectionView)CollectionViewSource.GetDefaultView(FemalesObservableList);
            _view.Filter = CustomFilter;

            _backButton = backButton;
            _editAndDelete = editAndDelete;

        }

        public MainFemales()
        {
            InitializeComponent();
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

        private void GetFemalesFromDataBase()
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _editAndDelete.IsEnabled = FemalesList.SelectedItem != null;
            DatabaseFirst.Females female = (DatabaseFirst.Females)FemalesList.SelectedItem;
            _selectedFemale = female;

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

        private bool CustomFilter(object obj)
        {
            if (String.IsNullOrEmpty(TextBox.Text))
                return true;
            else
            {
                return (((DatabaseFirst.Females)obj).code.IndexOf(TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Females)obj).birthday.IndexOf(TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Females)obj).martenity.ToString().IndexOf(TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Females)obj).status.IndexOf(TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(FemalesList.ItemsSource).Refresh();
        }

        public void RemoveSelectedItem()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.Females.RemoveFemaleByCode(_selectedFemale.code);
            unitOfWork.Complete();

            MainGridManager.SetUserControl(new MainFemales());
            //RemoveItemFromList(FemalesObservableList, _selectedFemale);

        }
    }
}
