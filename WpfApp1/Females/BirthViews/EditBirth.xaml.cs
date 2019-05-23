using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.BirthViews
{
    /// <summary>
    /// Interaction logic for EditBirth.xaml
    /// </summary>
    public partial class EditBirth : UserControl
    {

        private Births _birth;

        public delegate void BirthEditedEventHandler(object source, BirthsEventArgs args);

        public event BirthEditedEventHandler BirthEdited;

        public EditBirth()
        {
            InitializeComponent();
        }

        public virtual void OnBirthEdited(Births birth)
        {
            var events = new BirthsEventArgs { Birth = birth };
            BirthEdited?.Invoke(this, events);
            //Using null propagation
            //if (VideoEncoded != null)
            //{
            //    VideoEncoded(this, EventArgs.Empty);
            //}
        }
        public EditBirth(Births birth)
        {
            InitializeComponent();
            _birth = birth;
            UserAgree.AcceptButton = Accept_btn;
            GetBirth();


        }
        private void GetBirth()
        {
            NPigletsTextBox.Text = _birth.n_piglets.ToString();
            DatePicker.Text = _birth.date;
        }

        private void EditBirthFromDatabase(string newN, string newDate)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var birth = unitOfWork.Births.Get(_birth.id);
            birth.n_piglets = Int32.Parse(newN);
            birth.date = newDate;
            unitOfWork.Complete();

            OnBirthEdited(birth);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Accept_btn_OnClick(object sender, RoutedEventArgs e)
        {
            EditBirthFromDatabase(NPigletsTextBox.Text, DatePicker.Text);
        }
    }
}
