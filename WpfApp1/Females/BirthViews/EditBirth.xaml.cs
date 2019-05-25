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
        public Births Birth { get; }

        public event EventHandler<BirthsEventArgs> BirthEdited;

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
            Birth = birth;
            GetBirth();
            UserAgree.AcceptButton = Accept_btn;

        }
        private void GetBirth()
        {
            NPigletsTextBox.Text = Birth.n_piglets.ToString();
            DeadPigsTextBox.Text = Birth.died_piglets.ToString();
            MummysTextBox.Text = Birth.mummys.ToString();
            DatePicker.Text = Birth.date;
        }

        private void EditBirthFromDatabase(string newN, string newDate, string newDead, string newMummys)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var birth = unitOfWork.Births.Get(Birth.id);
            birth.n_piglets = Int32.Parse(newN);
            birth.date = newDate;
            birth.died_piglets = Int32.Parse(newDead);
            birth.mummys = Int32.Parse(newMummys);
            unitOfWork.Complete();

            OnBirthEdited(birth);

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Accept_btn_OnClick(object sender, RoutedEventArgs e)
        {
            EditBirthFromDatabase(NPigletsTextBox.Text, DatePicker.Text, DeadPigsTextBox.Text, MummysTextBox.Text);
        }
    }
}
