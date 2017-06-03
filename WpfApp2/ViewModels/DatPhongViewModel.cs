 using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2.Services;

namespace WpfApp2.ViewModels
{
    class DatPhongViewModel: ViewModelBase
    {
        public ObservableCollection<PhongViewModel> DanhMucPhong { get; set; }

        public ICommand LapPhieuThuePhongCommand { get; private set; }

        private async void LoadData()
        {
            ObservableCollection<PhongViewModel> _danhSachPhong = new ObservableCollection<PhongViewModel>();
            using (var db = new KhachSanContext())
            {
                var danhMucPhong = await (from p in db.PHONGs
                                          join lp in db.LOAIPHONGs on p.MALOAIPHONG equals lp.MALOAIPHONG
                                          orderby p.MAPHONG
                                          select new { p.TENPHONG, p.LOAIPHONG, lp.TENLOAIPHONG, lp.DONGIA, p.TINHTRANG, p.GHICHU }).ToListAsync();
                foreach (var phong in danhMucPhong)
                {
                    PhongViewModel phongViewModel = new PhongViewModel { Phong = new PHONG { TENPHONG = phong.TENPHONG, LOAIPHONG = phong.LOAIPHONG, GHICHU = phong.GHICHU, TINHTRANG = phong.TINHTRANG } };
                    _danhSachPhong.Add(phongViewModel);
                }
                DanhMucPhong = _danhSachPhong;
                RaisePropertyChanged("DanhMucPhong");
            }

        }
        public DatPhongViewModel()
        {
            LoadData();
            LapPhieuThuePhongCommand = new RelayCommand<String>(ExecuteLapPhieuThuePhong);
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);

        }

        {
            if (msg.Notification == "Close window")
            {
                LoadData();
            }
        }

        private void ExecuteLapPhieuThuePhong(String tenPhong)
        {
            KhachSanContext db = new KhachSanContext();
            var phong =  db.PHONGs.ToList().Find(p => p.TENPHONG == tenPhong);
            if (phong.TINHTRANG == "Trống")
            {
                showWindow(tenPhong);
            }
            else
            {
                //show   
            }

        }

        public void showWindow(string tenPhong)
        {
            Messenger.Default.Send(new NotificationMessage(tenPhong));
            
        }
    }
}
