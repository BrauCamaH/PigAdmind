using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.SickViews
{
    /// <summary>
    /// Lógica de interacción para SicksPage.xaml
    /// </summary>
    public partial class SicksPage : UserControl, IPigAdmindPage<Sicks, SicksEventArgs>
    {
        private ObservableCollection<Sicks> _sicksObservableCollection;

        public DatabaseFirst.Females Female { get; set; }

        private DeleteSick _deleteSick;
        private EditSick _editSick;
        public Sicks CurrenSick { get; set; }

        public SicksPage()
        {
            InitializeComponent();
            _sicksObservableCollection = new ObservableCollection<Sicks>();
        }


        public void InitializeCrudControls(Sicks entity)
        {
            _deleteSick = new DeleteSick(entity);
            _editSick = new EditSick(entity);

            EditAndDelete.DeleteControl = _deleteSick;
            EditAndDelete.EditControl = _editSick;

            _editSick.SickEdited += OnItemEdited;
            _deleteSick.SickDeleted += OnItemDeleted;

        }

        public bool CustomFilter(object obj)
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

        public void SetFemale(DatabaseFirst.Females female)
        {
            Female = female;
            GetItemsFromDatabase();
        }

        public void GetItemsFromDatabase()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            CrudOperations<Births>.AddRange(_sicksObservableCollection, unitOfWork.Sicks.GetSicksByFemale(Female.code));
            SicksListView.ItemsSource = _sicksObservableCollection;
        }

        public void OnItemAdded(object sender, SicksEventArgs e)
        {

            _sicksObservableCollection.Add(e.Sick);
        }

        public void OnItemDeleted(object sender, SicksEventArgs e)
        {
            try
            {
                _sicksObservableCollection.Remove(_sicksObservableCollection.Single(s => s.id == e.Sick.id));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void OnItemEdited(object sender, SicksEventArgs e)
        {
            _sicksObservableCollection[_sicksObservableCollection.IndexOf(CurrenSick)] = e.Sick;
        }


        private void SicksListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditAndDelete.IsEnabled = SicksListView.SelectedItem != null;

            Sicks sick = (Sicks)SicksListView.SelectedItem;
            CurrenSick = sick;

            var unitOfWork = new UnitOfWork(new Entities());
            if (sick != null) InitializeCrudControls(unitOfWork.Sicks.Get(sick.id));
        }
    }
}
