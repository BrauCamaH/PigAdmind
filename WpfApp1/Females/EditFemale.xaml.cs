using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for EditFemale.xaml
    /// </summary>
    public partial class EditFemale : UserControl
    {
        private DatabaseFirst.Females _female;


        public event EventHandler<FemalesEventArgs> FemaleEdited;
        public EditFemale()
        {
            InitializeComponent();

        }

        public virtual void OnFemaleEdited(DatabaseFirst.Females female)
        {
            FemaleEdited?.Invoke(this, new FemalesEventArgs { Female = female });
        }
        public EditFemale(DatabaseFirst.Females female)
        {
            InitializeComponent();
            _female = female;
            GetFemale(female);
            NotifyUserAgree.AcceptButton = AcceptBtn;

        }

        private void EditFemaleFromDatabase(string newDate)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var female = unitOfWork.Females.GetFemaleByCode(_female.code);
            female.birthday = newDate;
            unitOfWork.Complete();

            OnFemaleEdited(female);
        }

        private void GetFemale(DatabaseFirst.Females female)
        {
            //CodeTextBox.Text = _female.code;
            NewDateDatePicker.Text = _female.birthday;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AcceptBtn_OnClick(object sender, RoutedEventArgs e)
        {
            EditFemaleFromDatabase(NewDateDatePicker.Text);
        }
    }
}

