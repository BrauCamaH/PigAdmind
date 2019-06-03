using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for AddBirth.xaml
    /// </summary>
    public partial class AddBirth : UserControl
    {
        private DatabaseFirst.Females _female;

        public event EventHandler<BirthsEventArgs> BirthAdded;


        public virtual void OnBirthAdded(Births birth)
        {
            BirthAdded?.Invoke(this, new BirthsEventArgs { Birth = birth });

        }
        public AddBirth()
        {
            InitializeComponent();
        }

        public AddBirth(DatabaseFirst.Females female)
        {
            InitializeComponent();
            _female = female;

        }


        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var female = unitOfWork.Females.GetFemaleByCode(_female.code);

            var birth = new Births()
            {
                n_piglets = Int32.Parse(N_Pigs_TextBox.Text),
                date = Date.Text,
                fem_code = _female.code,
                died_piglets = Int32.Parse(DeadPigsTextBox.Text),
                mummys = Int32.Parse(MummysTextBox.Text),
                status = "Actual"
            };

            female.status = "Madre";

            unitOfWork.Births.Add(birth);
            unitOfWork.Complete();
            OnBirthAdded(birth);
        }
    }
}
