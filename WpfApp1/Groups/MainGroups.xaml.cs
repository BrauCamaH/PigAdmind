﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp1.DatabaseFirst;
using WpfApp1.Interfaces;
using WpfApp1.Managers;

namespace WpfApp1.Groups
{
    /// <summary>
    /// Interaction logic for MainGroups.xaml
    /// </summary>
    public partial class MainGroups : UserControl, IRemovable
    {
        private readonly ObservableCollection<PigGroups> GroupsObservableList = new ObservableCollection<PigGroups>();
        public MainGroups()
        {
            InitializeComponent();
            GetGroupsFromDataBase();

            SearchTextBox.SetView(GroupList, CustomFilter);
        }

        private bool CustomFilter(object obj)
        {
            if (String.IsNullOrEmpty(SearchTextBox.TextBox.Text))
                return true;
            else
            {
                return (((DatabaseFirst.PigGroups)obj).name.IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.PigGroups)obj).pig_count.ToString().IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.PigGroups)obj).weigth_avg.ToString().IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.PigGroups)obj).weaning_date.IndexOf(SearchTextBox.TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }


        private bool IsDataComplete(string txtbox, string datePicker)
        {
            if (txtbox != "" && datePicker != "")
            {
                return true;
            }
            return false;
        }
        private void GetGroupsFromDataBase()
        {
            var unitOfWork = new Entities();
            foreach (var pigGroups in unitOfWork.PigGroups.ToList())
            {
                if (GroupsObservableList.Count < unitOfWork.PigGroups.ToList().Count)
                {
                    GroupsObservableList.Add(pigGroups);
                }
            }
            GroupList.ItemsSource = GroupsObservableList;
        }

        private void AddNewGroup(string id, float weigth, int n, string date)
        {
            var ctx = new Entities();
            var pigGroup = new DatabaseFirst.PigGroups()
            {
                weaning_date = date,
                weigth_avg = weigth,
                second_avg = 0,
                lastWeigth_avg = 0,
                pig_count = n,
                died_pigs = 0,
                user = 1,
                name = id
            };
            GroupsObservableList.Add(pigGroup);
            ctx.PigGroups.Add(pigGroup);
            ctx.SaveChanges();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void OnListMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            MainGridManager.SetUserControl(new GroupPage());
        }

        private void ClearFields()
        {
            NPigsBox.Text = "";
            WeigthAVGBox.Text = "";
            DatePicker.Text = "";
            IdBox.Text = "";
        }

        private void AddNewGroup_OnClick(object sender, RoutedEventArgs e)
        {
            int nPigs = Int32.Parse(NPigsBox.Text);
            float weigth = float.Parse(WeigthAVGBox.Text);
            string weanigDate = DatePicker.Text;
            string id = IdBox.Text;
            if (IsDataComplete(NPigsBox.Text, weanigDate))
            {
                AddNewGroup(id, weigth, nPigs, weanigDate);
                ClearFields();
            }

        }


        private void LookTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(GroupList.ItemsSource).Refresh();
        }

        public void RemoveSelectedItem()
        {
            var unitOfWork = new Entities();
            unitOfWork.PigGroups.Remove(unitOfWork.PigGroups.ToList()[GroupList.SelectedIndex]);
            GroupsObservableList.RemoveAt(GroupList.SelectedIndex);
            unitOfWork.SaveChanges();
        }
    }
}
