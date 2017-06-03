using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp2.ViewModels
{
    class HoaDonViewModel
    {
        public string TenPhong { get; set; }

        public string LoaiPhong { get; set; }

        public string SoNgayThue { get; set; }

        public int DonGia { get; set; }
        public ObservableCollection<KhachHangViewModel> DanhSachKhachHang { get; set; }

        public int ThanhTien { get; set; }


        public ICommand LuuHoaDonCommand { get; set; }

        public ICommand HuyCommand { get; set; }


        void CloseWindow()
        {
            Messenger.Default.Send(new OpenWindowMessage() { WindowName = View.HoaDon, Message="Close Window"});
        }

        public HoaDonViewModel(string tenPhong)
        {
            TenPhong = tenPhong;
            HuyCommand = new RelayCommand(CloseWindow);
            LoadData();
        }

        private void LoadData()
        {
            ObservableCollection<KhachHangViewModel> danhSachKhachHang = new ObservableCollection<KhachHangViewModel>();
            KhachSanContext db = new KhachSanContext();
            var thongTinPhong = db.PHONGs.ToList().Find(p => p.TENPHONG == TenPhong);
            LoaiPhong = thongTinPhong.LOAIPHONG.TENLOAIPHONG;
            DonGia = thongTinPhong.LOAIPHONG.DONGIA;

            var danhSachKhachHangDB = (from kh in db.KHACHHANGs
                                     where kh.PHONG.TENPHONG == TenPhong
                                     select new { kh.TENKHACHHANG, kh.LOAIKHACH.TENLOAIKHACH, kh.CMND, kh.DIACHI }).ToList();
            foreach(var KH in danhSachKhachHangDB)
            {
                danhSachKhachHang.Add(new KhachHangViewModel { TenKhachHang = KH.TENKHACHHANG, LoaiKhach = KH.TENLOAIKHACH, CMND = KH.CMND, DiaChi = KH.DIACHI });
            }

            DanhSachKhachHang = danhSachKhachHang;


        }

        
    }
}
