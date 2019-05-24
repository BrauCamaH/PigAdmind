using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.BirthViews
{
    /// <summary>
    /// Interaction logic for DeleteSelectedBirth.xaml
    /// </summary>
    public partial class DeleteBirth : UserControl
    {

        private Births _birth;

        public event EventHandler<BirthsEventArgs> BirthDeleted;

        public virtual void OnBirthDeleted(Births birth)
        {
            BirthDeleted?.Invoke(this, new BirthsEventArgs { Birth = birth });
        }

        public DeleteBirth()
        {
            InitializeComponent();

        }
        public DeleteBirth(Births births)
        {
            InitializeComponent();
            NotifyUserAgree.AcceptButton = Accept_btn;
            _birth = births;
        }

        private void Delete()
        {
            var unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.Births.RemoveByID(_birth.id);
            unitOfWork.Complete();
        }

        private void Accept_Btn_Click(object sender, RoutedEventArgs e)
        {
            Delete();
            OnBirthDeleted(_birth);
        }

    }
}
