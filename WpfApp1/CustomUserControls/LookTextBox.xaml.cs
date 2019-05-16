using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp1.CustomUserControls
{
    /// <summary>
    /// Lógica de interacción para LookTextBox.xaml
    /// </summary>
    public partial class LookTextBox : UserControl
    {
        private CollectionView _view;
        private ListView _listView;
        public LookTextBox()
        {
            InitializeComponent();
        }

        public void SetView(ListView view, Predicate<object> filter)
        {
            _listView = view;
            _view = (CollectionView)CollectionViewSource.GetDefaultView(view.ItemsSource);
            _view.Filter = filter;
        }

        private void LookButton_OnClick(object sender, RoutedEventArgs e)
        {
            TextBox.Text = "";
        }


        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(_listView.ItemsSource).Refresh();
        }
    }
}
