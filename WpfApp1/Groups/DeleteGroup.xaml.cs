using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Groups
{
    /// <summary>
    /// Lógica de interacción para DeleteGroup.xaml
    /// </summary>
    public partial class DeleteGroup : UserControl
    {
        private PigGroups _pigGroup;

        public event EventHandler<GroupsEventArgs> GroupDeleted;

        public DeleteGroup()
        {
            InitializeComponent();
        }
        public DeleteGroup(PigGroups pigGroup)
        {
            _pigGroup = pigGroup;
            InitializeComponent();

            NotifyUserAgree.AcceptButton = Accept_btn;
        }

        public virtual void OnGroupDeleted(PigGroups group)
        {
            GroupDeleted?.Invoke(this, new GroupsEventArgs { Group = group });
        }

        private void Delete()
        {
            var unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.Groups.RemoveGroupById(_pigGroup.id);
            unitOfWork.Complete();
        }

        private void Accept_Btn_Click(object sender, RoutedEventArgs e)
        {
            Delete();
            OnGroupDeleted(_pigGroup);
        }
    }
}
