using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Lógica de interacción para MisbirthEvent.xaml
    /// </summary>
    public partial class MisbirthEvent : UserControl
    {
        private DatabaseFirst.Females _female;

        public event EventHandler<FemalesEventArgs> StatusModified;

        public event EventHandler<InseminationsEventArgs> InseminationModified;
        public MisbirthEvent(DatabaseFirst.Females female)
        {
            _female = female;
            InitializeComponent();
        }

        public virtual void OnStatusModified(DatabaseFirst.Females female)
        {
            StatusModified?.Invoke(this, new FemalesEventArgs { Female = female });
        }

        public virtual void OnInseminationModified(Inseminations insemination)
        {
            InseminationModified?.Invoke(this, new InseminationsEventArgs { Insemination = insemination });
        }
        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entities());
                var female = unitOfWork.Females.GetFemaleByCode(_female.code);

                var insemination = unitOfWork.Inseminations.GetCurrentInsemination(female);

                if (insemination == null)
                {
                    insemination = unitOfWork.Inseminations.GetLastSuccessInsemination(female);
                }

                insemination.status = "Fallida";
                female.status = "Abortada";
                unitOfWork.Complete();

                OnStatusModified(female);
                OnInseminationModified(insemination);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }
    }
}
