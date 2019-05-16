using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.BirthViews
{
    /// <summary>
    /// Lógica de interacción para BirthsPage.xaml
    /// </summary>
    public partial class BirthsPage : UserControl
    {
        public ObservableCollection<Births> BirthsObservableList { get; set; }
        public DatabaseFirst.Females Female { get; private set; }

        public Births CurrentBirth { get; private set; }

        public BirthsPage()
        {
            InitializeComponent();
            BirthsObservableList = new ObservableCollection<Births>();
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

        public void SetFemale(DatabaseFirst.Females female)
        {
            Female = female;
            GetBirthsFromDatabase();
            SearchBox.SetView(BirthsListView, CustomFilter);
        }
        private void EditBirthButton_OnClick(object sender, RoutedEventArgs e)
        {


        }

        private void DeleteBirthButton_Onclick(object sender, RoutedEventArgs e)
        {
            EventDialog.IsOpen = true;
            MainGridEvent.Children.Clear();
            MainGridEvent.Children.Add(new DeleteBirth(CurrentBirth, BirthsObservableList));
        }

        public static void AddRange<T>(ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Add(item);
            }
        }

        private void GetBirthsFromDatabase()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            AddRange(BirthsObservableList, unitOfWork.Births.GetBirthsByFemale(Female.code) as IEnumerable<Births>);
            BirthsListView.ItemsSource = BirthsObservableList;
        }

        private void BirthsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Births birth = (Births)BirthsListView.SelectedItem;
            CurrentBirth = birth;
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(BirthsListView.ItemsSource).Refresh();
        }
    }
}
