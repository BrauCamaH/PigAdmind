using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para User.xaml
    /// </summary>
    public partial class User : Window
    {
        private Users _user;

        private readonly SnackbarMessageQueue messageQueue =
            new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
        public User()
        {
            InitializeComponent();

            var unitOfWork = new Entities();
            var user = unitOfWork.Users.FirstOrDefault();
            _user = user;

            GetEmailFromDatabase();
        }

        private void ShowSnackbar(Snackbar snackbar, string message)
        {
            messageQueue.Enqueue(message);
            snackbar.MessageQueue = messageQueue;
        }


        private void GetEmailFromDatabase()
        {
            if (_user != null) EmailTextBox.Text = _user.email;
        }

        private void ChangeEmail(string email)
        {
            var unitOfWork = new Entities();
            var user = unitOfWork.Users.FirstOrDefault();
            if (user != null && !user.email.Equals(email))
            {
                user.email = email;
            }

            unitOfWork.SaveChanges();
        }


        private bool CanChangePassword(string pass, string again)
        {
            if (pass.Equals(again))
            {
                var unitOfWork = new Entities();
                var user = unitOfWork.Users.FirstOrDefault();
                if (user != null) user.password = pass;
                unitOfWork.SaveChanges();
                return true;
            }

            if (pass == "")
            {
                return true;
            }
            else
            {
                ShowSnackbar(UserConfigSnackBar, "Las contraseñas no coinciden");
                return false;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeEmail(EmailTextBox.Text);
            if (CanChangePassword(PasswordBox.Password, AgainPasswordBox.Password))
            {
                new MainWindow().Show();
                Close();
            }

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void MaterialDesignOutlinedPasswordFieldPasswordBoxEnabledComboBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Clear();
            AgainPasswordBox.Clear();
        }

        private void MaterialDesignFilledTextFieldTextBoxEnabledComboBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            EmailTextBox.Text = _user.email;
        }
    }
}
