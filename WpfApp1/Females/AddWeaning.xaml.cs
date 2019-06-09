using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for AddWeaning.xaml
    /// </summary>
    public partial class AddWeaning : UserControl
    {
        private DatabaseFirst.Females _female;

        public event EventHandler<Weaning> WeaningAdded;

        public event EventHandler<FemalesEventArgs> StatusModified;

        public event EventHandler<BirthsEventArgs> BirthModified;
        public AddWeaning(DatabaseFirst.Females female)
        {
            _female = female;
            InitializeComponent();
        }

        public virtual void OnBirthModified(Births e)
        {
            BirthModified?.Invoke(this, new BirthsEventArgs { Birth = e });
        }
        public virtual void OnWeaningAdded(Weaning weaning)
        {
            try
            {
                var unitOfWork = new UnitOfWork(new Entities());
                var birth = unitOfWork.Births.GetCurrentBirth(_female);
                birth.weaning = weaning.id;

                birth.status = "Destetado";
                unitOfWork.Complete();

                OnBirthModified(birth);
                WeaningAdded?.Invoke(this, weaning);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public virtual void OnStatusModified(DatabaseFirst.Females female)
        {
            StatusModified?.Invoke(this, new FemalesEventArgs { Female = female });
        }
        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var unitOfWork = new UnitOfWork(new Entities());
                var female = unitOfWork.Females.GetFemaleByCode(_female.code);

                var weaning = new Weaning
                {
                    weaned_pigs = int.Parse(NPigsTextBox.Text),
                    date = DatePicker.Text
                };

                female.status = "Destetada";

                OnStatusModified(female);

                unitOfWork.Weaning.Add(weaning);
                unitOfWork.Complete();

                OnWeaningAdded(weaning);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }


    }
}
