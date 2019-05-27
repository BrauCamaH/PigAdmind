using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.CustomUserControls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.InseminationViews
{
    /// <summary>
    /// Lógica de interacción para InseminationsPage.xaml
    /// </summary>
    public partial class InseminationsPage : UserControl
    {
        private ObservableCollection<Inseminations> _inseminationsObservableList;
        public DatabaseFirst.Females Female { get; private set; }

        public Inseminations CurrentInsemination { get; private set; }

        private EditInsemination _editInsemination;
        private DeleteInsemination _deleteInsemination;

        public InseminationsPage()
        {
            InitializeComponent();
            _inseminationsObservableList = new ObservableCollection<Inseminations>();
        }

        //This method set the female to make possible all the expected behaviour
        public void SetFemale(DatabaseFirst.Females female)
        {
            Female = female;
            GetInseminationsFromDatabase();
            SearchBox.SetView(InseminationsListView, CustomFilter);

        }

        private bool CustomFilter(object obj)
        {
            if (String.IsNullOrEmpty(SearchBox.TextBox.Text))
                return true;
            else
            {
                return (((DatabaseFirst.Inseminations)obj).male_code.ToString()
                        .IndexOf(SearchBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Inseminations)obj).date.IndexOf(SearchBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Inseminations)obj).status.IndexOf(SearchBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }
        private void GetInseminationsFromDatabase()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            CrudOperations<Inseminations>.AddRange(_inseminationsObservableList, unitOfWork.Inseminations.GetInseminationsByFemale(Female.code));
            InseminationsListView.ItemsSource = _inseminationsObservableList;
        }


        private void InsminationsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditAndDelete.IsEnabled = InseminationsListView.SelectedItem != null;

            Inseminations insemination = (Inseminations)InseminationsListView.SelectedItem;
            CurrentInsemination = insemination;
            var unitOfWork = new UnitOfWork(new Entities());
            if (insemination != null) InitializeCrudControls(unitOfWork.Inseminations.Get(insemination.id));
        }

        private void InitializeCrudControls(Inseminations insemination)
        {
            _editInsemination = new EditInsemination(insemination);
            _deleteInsemination = new DeleteInsemination(insemination);

            EditAndDelete.EditControl = _editInsemination;
            EditAndDelete.DeleteControl = _deleteInsemination;

            _deleteInsemination.InseminationDeleted += OnInseminationDeleted;
            _editInsemination.InseminationEdited += OnInseminationEdited;
        }

        private void OnInseminationDeleted(object sender, InseminationsEventArgs e)
        {
            RemoveItemFromList(_inseminationsObservableList, e.Insemination);
        }

        public void OnInseminationAdded(object sender, InseminationsEventArgs e)
        {
            _inseminationsObservableList.Add(e.Insemination);
        }

        private void OnInseminationEdited(object sender, InseminationsEventArgs e)
        {
            try
            {
                _inseminationsObservableList[_inseminationsObservableList.IndexOf(CurrentInsemination)] = e.Insemination;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                //                MessageBox.Show(exception.StackTrace);
                //                MessageBox.Show(exception.Source);
            }

        }
        private void RemoveItemFromList(ObservableCollection<Inseminations> collection, Inseminations insemination)
        {
            collection.Remove(collection.Single(i => i.id == insemination.id));
            //MessageBox.Show("Item Deleted");
        }
    }
}
