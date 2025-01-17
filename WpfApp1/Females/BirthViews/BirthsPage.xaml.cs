﻿using System;
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

        private void BirthsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditAndDelete.IsEnabled = BirthsListView.SelectedItem != null;

            Births birth = (Births)BirthsListView.SelectedItem;
            CurrentBirth = birth;
            UpdateWeaning(birth);

            var unitOfWork = new UnitOfWork(new Entities());
            if (birth != null) InitializeCrudControls(unitOfWork.Births.Get(birth.id));

        }
        public void OnBirthAdded(object sender, BirthsEventArgs e)
        {
            _birthsObservableList.Add(e.Birth);
        }

        private void UpdateWeaning(Births birth)
        {
            try
            {
                if (birth != null)
                {
                    var unitOfWork = new UnitOfWork(new Entities());
                    var weaning = unitOfWork.Births.GetWeaning(birth);
                    DateTextBlock.Text = weaning.date;
                    NPigsTextBlock.Text = weaning.weaned_pigs.ToString();
                    WeaningInfoTable.IsEnabled = true;
                }
                else
                {
                    DateTextBlock.Text = "No se ha destetado";
                    NPigsTextBlock.Text = "0";
                    WeaningInfoTable.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                DateTextBlock.Text = "No se ha destetado";
                NPigsTextBlock.Text = "0";
                WeaningInfoTable.IsEnabled = false;
            }
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

        public void OnBirthEdited(object sender, BirthsEventArgs e)
        {
            try
            {
                var birthIndex = _birthsObservableList.IndexOf(_birthsObservableList.FirstOrDefault(i => i.id == e.Birth.id));
                _birthsObservableList[birthIndex] = e.Birth;
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

        private void EditWeaning_OnClick(object sender, RoutedEventArgs e)
        {
            var usc = new EditWeaning(CurrentBirth);
            MainDialogHost.Instance.SetNewUserControl(usc);

            usc.BirthEdited += OnWeaningEdited;
        }

        private void OnWeaningEdited(object sender, BirthsEventArgs e)
        {
            UpdateWeaning(e.Birth);
        }
    }
}
