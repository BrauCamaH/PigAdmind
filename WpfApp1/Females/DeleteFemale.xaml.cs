using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Lógica de interacción para DeleteFemale.xaml
    /// </summary>
    public partial class DeleteFemale : UserControl
    {
        private DatabaseFirst.Females _female;

        public event EventHandler<FemalesEventArgs> FemaleDeleted;
        public DeleteFemale()
        {
            InitializeComponent();
        }

        public DeleteFemale(DatabaseFirst.Females female)
        {
            InitializeComponent();

            UserAgree.AcceptButton = Accept_btn;
            _female = female;
        }

        public virtual void OnFemaleDeleted(DatabaseFirst.Females female)
        {
            FemaleDeleted?.Invoke(this, new FemalesEventArgs { Female = female });
        }
        private void Delete()
        {
            var unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.Females.RemoveFemaleByCode(_female.code);
            unitOfWork.Complete();
        }
        private void Accept_Btn_Click(object sender, RoutedEventArgs e)
        {
            Delete();
            OnFemaleDeleted(_female);
        }
    }
}
