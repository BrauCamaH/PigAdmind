using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.CustomUserControls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.BirthViews
{
    /// <summary>
    /// Lógica de interacción para BirthsPage.xaml
    /// </summary>
    public partial class BirthsPage : UserControl
    {
        private ObservableCollection<Births> _birthsObservableList;
        public DatabaseFirst.Females Female { get; private set; }

        public Births CurrentBirth { get; private set; }

        private DeleteBirth _deleteBirth;
        private EditBirth _editBirth;


        public BirthsPage()
        {
            InitializeComponent();
            _birthsObservableList = new ObservableCollection<Births>();
        }

        private void InitializeCrudControls(Births birth)
        {
            _editBirth = new EditBirth(birth);
            _deleteBirth = new DeleteBirth(birth);

            EditAndDelete.EditControl = _editBirth;

            EditAndDelete.DeleteControl = _deleteBirth;
            _deleteBirth.BirthDeleted += OnBirthDeleted;
            _editBirth.BirthEdited += OnBirthEdited;
        }
        private bool CustomFilter(object obj)
        {
            if (String.IsNullOrEmpty(SearchBox.TextBox.Text))
                return true;
            else
            {
                return (((DatabaseFirst.Births)obj).n_piglets.ToString()
                        .IndexOf(SearchBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Births)obj).date.IndexOf(SearchBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }

        //This method set the female to make possible all the expected behaviour
        public void SetFemale(DatabaseFirst.Females female)
        {
            Female = female;
            GetBirthsFromDatabase();
            SearchBox.SetView(BirthsListView, CustomFilter);

        }

        private void GetBirthsFromDatabase()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            CrudOperations<Births>.AddRange(_birthsObservableList, unitOfWork.Births.GetBirthsByFemale(Female.code).ToList());
            BirthsListView.ItemsSource = _birthsObservableList;
        }

        private void SetInfoTableVisible(bool isVisible)
        {
            WeaningInfoTable.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }
        private void BirthsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditAndDelete.IsEnabled = BirthsListView.SelectedItem != null;
            SetInfoTableVisible(BirthsListView != null);

            Births birth = (Births)BirthsListView.SelectedItem;
            CurrentBirth = birth;
            var unitOfWork = new UnitOfWork(new Entities());
            if (birth != null) InitializeCrudControls(unitOfWork.Births.Get(birth.id));
        }
        public void OnBirthAdded(object sender, BirthsEventArgs e)
        {
            _birthsObservableList.Add(e.Birth);
        }

        private void OnBirthDeleted(object sender, BirthsEventArgs e)
        {
            try
            {
                RemoveItemFromList(_birthsObservableList, e.Birth);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }


        }

        private void OnBirthEdited(object sender, BirthsEventArgs e)
        {
            try
            {
                _birthsObservableList[_birthsObservableList.IndexOf(CurrentBirth)] = e.Birth;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                //                MessageBox.Show(exception.StackTrace);
                //                MessageBox.Show(exception.Source);
            }

        }

        private void RemoveItemFromList(ObservableCollection<Births> collection, Births birth)
        {
            collection.Remove(collection.Single(i => i.id == birth.id));
            //MessageBox.Show("Item Deleted");
        }


    }
}
