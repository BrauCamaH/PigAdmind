using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly ObservableCollection<Births> _birthsObservableList;
        public DatabaseFirst.Females Female { get; private set; }

        public Births CurrentBirth { get; private set; }



        public BirthsPage()
        {
            InitializeComponent();
            _birthsObservableList = new ObservableCollection<Births>();

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
            CrudOperations<Births>.AddRange(_birthsObservableList, unitOfWork.Births.GetBirthsByFemale(Female.code) as IEnumerable<Births>);
            BirthsListView.ItemsSource = _birthsObservableList;
        }

        private void BirthsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditAndDelete.IsEnabled = BirthsListView.SelectedItem != null;

            Births birth = (Births)BirthsListView.SelectedItem;
            CurrentBirth = birth;

            var delete = new DeleteBirth(CurrentBirth, _birthsObservableList);

            EditAndDelete.DeleteControl = delete;

            //            EditBirth editBirth = new EditBirth(CurrentBirth);
            //            EditAndDelete.EditControl = editBirth;

            delete.BirthDeleted += OnBirthDeleted;


        }
        public void OnBirthAdded(object sender, BirthsEventArgs e)
        {
            _birthsObservableList.Add(e.Birth);
        }
        private void OnBirthDeleted(object sender, BirthsEventArgs e)
        {
            RemoveItemFromList(_birthsObservableList, e.Birth);
        }

        private void RemoveItemFromList(ObservableCollection<Births> collection, Births birth)
        {
            collection.Remove(collection.Single(i => i.id == birth.id));
        }



    }
}
