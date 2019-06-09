using MaterialDesignThemes.Wpf;
using Notifications.Wpf;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.DatabaseFirst;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));
        public Login()
        {
            InitializeComponent();
            //The checbox is checked when the user is online
            CheckBox.IsChecked = App.User.isOnline == 1;
        }

        private void ShowSnackbar(string message)
        {
            messageQueue.Enqueue(message);
            LoginSnackbar.MessageQueue = messageQueue;
        }

        private bool IsTheCorrectPassword(string inputPass)
        {
            var ctx = new Entities();
            var user = ctx.Users.SqlQuery("select * from users").First<Users>();

            var pass = user.password.Replace(" ", "");
            if (pass == inputPass)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsTheCorrectPassword(PasswordBox.Password))
            {
                SetIsOnline(CheckBox);
                var window = new MainWindow();
                window.Show();
                Close();
            }
            else
            {
                ShowSnackbar("La contraseña es incorrecta");
            }

        }
        private void SetIsOnline(CheckBox checkBox)
        {
            var ctx = new Entities();
            var user = ctx.Users.First<Users>();
            if (checkBox.IsChecked == true)
            {
                user.isOnline = 1;
            }
            else
            {
                user.isOnline = 0;
            }
            ctx.SaveChanges();

        }


        private void SendEmail()
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("sistemaporcinopadm@gmail.com", "#pig87654321");

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("sistemaporcinopadm@gmail.com"),
                    Subject = "Test email.",
                    Body = "Test email body"
                };

                mail.To.Add(new MailAddress("braulio.camarenah@gmail.com"));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };

                // Send it...         
                client.Send(mail);
                var notificationManager = new NotificationManager();

                notificationManager.Show(new NotificationContent
                {
                    Title = "Recuperación completada",
                    Message = "Se ha enviado contraseña a su correo",
                    Type = NotificationType.Information
                });

            }
            catch (Exception ex)
            {


                var notificationManager = new NotificationManager();

                notificationManager.Show(new NotificationContent
                {
                    Title = "Error",
                    Message = ex.Message,
                    Type = NotificationType.Error
                });

            }

        }
        private void PasswordForgot_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(
                    "Esta seguro de eviar contraseña a su correo electrónico?"
                    , "Recuperar contraseña", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SendEmail();
                }
            }
            catch (Exception exception)
            {
                var notificationManager = new NotificationManager();

                notificationManager.Show(new NotificationContent
                {
                    Title = "Verifique señal de internet",
                    Message = exception.Message,
                    Type = NotificationType.Error
                });
            }


        }
    }
}
