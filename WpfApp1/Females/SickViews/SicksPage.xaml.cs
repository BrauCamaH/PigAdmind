using System.Collections.ObjectModel;
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

        public Sicks CurrenSick { get; set; }

        public SicksPage()
        {
            InitializeComponent();
            _sicksObservableCollection = new ObservableCollection<Sicks>();
        }


        public void InitializeCrudControls(Sicks entity)
        {
            throw new System.NotImplementedException();
        }

        public bool CustomFilter(object obj)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public void OnItemEdited(object sender, SicksEventArgs e)
        {
            throw new System.NotImplementedException();
        }


    }
}
