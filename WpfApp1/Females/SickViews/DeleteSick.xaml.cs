using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.SickViews
{
    /// <summary>
    /// Lógica de interacción para DeleteSick.xaml
    /// </summary>
    public partial class DeleteSick : UserControl
    {
        private Sicks _sick;

        public event EventHandler<SicksEventArgs> SickDeleted;
        public DeleteSick()
        {
            InitializeComponent();
        }

        public DeleteSick(Sicks sicks)
        {
            InitializeComponent();

            NotifyUserAgree.AcceptButton = Accept_btn;

            _sick = sicks;
        }

        public virtual void OnBirthDeleted(Sicks sick)
        {
            SickDeleted?.Invoke(this, new SicksEventArgs { Sick = sick });
        }

        private void Delete()
        {
            var unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.Sicks.RemoveById(_sick.id);
            unitOfWork.Complete();
        }

        private void Accept_Btn_Click(object sender, RoutedEventArgs e)
        {
            Delete();
            OnBirthDeleted(_sick);
        }
    }
}
