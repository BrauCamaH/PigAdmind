using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.InseminationViews
{
    /// <summary>
    /// Interaction logic for DeleteInsemination.xaml
    /// </summary>
    public partial class DeleteInsemination : UserControl
    {
        private Inseminations _insemination;

        public event EventHandler<InseminationsEventArgs> InseminationDeleted;

        public DeleteInsemination()
        {
            InitializeComponent();
        }

        public DeleteInsemination(Inseminations insemination)
        {
            InitializeComponent();
            NotifyUserAgree.AcceptButton = Accept_btn;
            _insemination = insemination;
        }
        public virtual void OnInseminationDeleted(Inseminations insemination)
        {
            InseminationDeleted?.Invoke(this, new InseminationsEventArgs { Insemination = insemination });
        }

        private void Delete()
        {
            var unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.Inseminations.RemoveById(_insemination.id);
            unitOfWork.Complete();
        }
        private void Accept_Btn_Click(object sender, RoutedEventArgs e)
        {
            Delete();
            OnInseminationDeleted(_insemination);
        }
    }
}
