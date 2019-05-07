using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        private readonly ObservableCollection<DatabaseFirst.Females> females = new ObservableCollection<DatabaseFirst.Females>();

        public MainFemales()
        {
            InitializeComponent();
            GetFemalesFromDataBase();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(FemalesList.ItemsSource);
            view.Filter = CustomFilter;
        }

        private void AddNewFemale(string code, string birthday)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            var female = new DatabaseFirst.Females()
            {
                code = code,
                birthday = birthday,
                status = "Normal",
                martenity = 0,
                user = 1,
                misbirths = 0

            };
            females.Add(female);
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
                if (females.Count < unitOfWork.Females.GetAll().Count())
                {
                    females.Add(female);
                }
            }
            FemalesList.ItemsSource = females;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FemalesList.SelectedItem != null)
            {
                MenuToolbarManager.SetEnableEditAndDelete(true);
                ContextManager.Instance().CurrentSelectedItem = FemalesList.SelectedIndex;
            }
            else
            {
                MenuToolbarManager.SetEnableEditAndDelete(false);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnListMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (FemalesList.SelectedItem != null)
            {
                MainGridManager.SetUserControl(new FemalePage(FemalesList.SelectedItem as DatabaseFirst.Females));
                MenuToolbarManager.SetEnableEditAndDelete(false);
                MenuToolbarManager.Back.IsEnabled = true;
                FemalesList.SelectedItem = null;
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
            unitOfWork.Females.Remove(unitOfWork.Females.GetAll().ToList()[FemalesList.SelectedIndex]);
            females.RemoveAt(FemalesList.SelectedIndex);
            unitOfWork.Complete();
        }
    }
}
