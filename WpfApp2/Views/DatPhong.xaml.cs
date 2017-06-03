using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.ViewModels;
namespace WpfApp2.Views
{
    /// <summary>
    /// Interaction logic for DatPhong.xaml
    /// </summary>
    public partial class DatPhong : UserControl
    {
        public DatPhong()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenWindowMessage>(this, NotificationMessageReceived);
        }
        
        private void NotificationMessageReceived(OpenWindowMessage msg)
        {
            if (msg.WindowName == View.PhieuThuePhong && msg.Message =="Open Window")
            {
                var LapPhieuThueView = new PhieuThuePhong();
                LapPhieuThueView.DataContext = new PhieuThueViewModel(tenPhong: msg.TenPhong);
                LapPhieuThueView.Show();
            }

            if(msg.WindowName == View.HoaDon && msg.Message =="Open Window")
            {
                var hoaDonView = new HoaDon();
                hoaDonView.DataContext = new HoaDonViewModel(tenPhong: msg.TenPhong);
                hoaDonView.Show();
            }
        }
        
    }
}
