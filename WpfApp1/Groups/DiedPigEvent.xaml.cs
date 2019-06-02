using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Groups
{
    /// <summary>
    /// Lógica de interacción para DiedPigEvent.xaml
    /// </summary>
    public partial class DiedPigEvent : UserControl
    {
        public DiedPigEvent()
        {
            InitializeComponent();
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void AddNumber(TextBox textBox)
        {
            int currentN = int.Parse(textBox.Text);
            currentN++;

            textBox.Text = currentN.ToString();
        }

        void SubtractNumber(TextBox textBox)
        {
            int currentN = int.Parse(textBox.Text);
            if (currentN > 1)
            {
                currentN--;
                textBox.Text = currentN.ToString();
            }
        }
        private void Minus_OnClick(object sender, RoutedEventArgs e)
        {
            SubtractNumber(NPigsTextBox);
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            AddNumber(NPigsTextBox);
        }
    }
}
