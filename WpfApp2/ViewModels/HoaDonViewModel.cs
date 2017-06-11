using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp2.ViewModels
{
    class HoaDonViewModel
    {
        public string TenPhong { get; set; }

        public string LoaiPhong { get; set; }

        public int SoNgayThue { get; set; }

        public string NgayThue { get; set; }
        public int DonGia { get; set; }
        public ObservableCollection<KhachHangViewModel> DanhSachKhachHang { get; set; }

        public Double ThanhTien { get; set; }


        public ICommand LuuHoaDonCommand { get; set; }

        public ICommand HuyCommand { get; set; }

        void CloseWindow()
        {
            Messenger.Default.Send(new OpenWindowMessage() { WindowName = View.HoaDon, Message="Close Window"});
        }

        public HoaDonViewModel(string tenPhong)
        {
            TenPhong = tenPhong;
            LuuHoaDonCommand = new RelayCommand(LuuHoaDonExcute);
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

            // Lay danh sach khach hang thue phong do lan cuoi cuoi de lap hoa don
            var phieuThueCuoi = db.PHIEUTHUEs.ToList().FindLast(p => p.PHONG.TENPHONG == thongTinPhong.TENPHONG);

            var danhSachKhachHangDB = (from kh in db.KHACHHANGs
                                       join ctpt in db.CTPTs on kh.MAKHACHHANG equals ctpt.MAKHACHHANG
                                       join pt in db.PHIEUTHUEs on ctpt.MAPHIEUTHUE equals pt.MAPHIEUTHUE
                                     where pt.MAPHIEUTHUE == phieuThueCuoi.MAPHIEUTHUE
                                     select new { kh.TENKHACHHANG, kh.LOAIKHACH.TENLOAIKHACH, kh.CMND, kh.DIACHI }).ToList();
            foreach(var KH in danhSachKhachHangDB)
            {
                danhSachKhachHang.Add(new KhachHangViewModel { TenKhachHang = KH.TENKHACHHANG, LoaiKhach = KH.TENLOAIKHACH, CMND = KH.CMND, DiaChi = KH.DIACHI });
            }

            DanhSachKhachHang = danhSachKhachHang;

            var phieuThue = db.PHIEUTHUEs.ToList().Find(p => p.MAPHONG == thongTinPhong.MAPHONG);
            DateTime ngayThue = phieuThue.NGAYTHUE;
            DateTime today = DateTime.Today;

            NgayThue = ngayThue.ToShortDateString();

            if((today - ngayThue).Days == 0)
            {
                SoNgayThue = 1;
            }
            else
            {
                SoNgayThue = (today - ngayThue).Days;
            }

            DonGia = thongTinPhong.LOAIPHONG.DONGIA;

            int soKhach = phieuThue.SOLUONGKHACH.Value;

            bool khachNuocNgoai = false;
            foreach(var kh in danhSachKhachHang)
            {
                if (kh.LoaiKhach == "Nước Ngoài")
                {
                    khachNuocNgoai = true;
                    break;
                }
            }

            if (khachNuocNgoai)
            {
                if(soKhach == 3)
                {
                    ThanhTien = (DonGia + DonGia * 0.25) * 1.5;
                }
                else
                {
                    ThanhTien = DonGia * 1.5;
                }
            }
            else
            {
                if (soKhach == 3)
                {
                    ThanhTien = (DonGia + DonGia * 0.25);
                }
                else
                {
                    ThanhTien = DonGia;
                }
            }

        }

        private void LuuHoaDonExcute()
        {
            KhachSanContext db = new KhachSanContext();

            var dsHoaDon = db.HOADONs.ToList();
            int maHD = 0;

            if (dsHoaDon.Count != 0)
            {
                maHD = dsHoaDon[dsHoaDon.Count - 1].MAHD + 1;
            }

            HOADON newHoaDon = new HOADON() { DIACHI = DanhSachKhachHang[0].DiaChi, SOLUONGKHACH = DanhSachKhachHang.Count, MAHD = maHD };

            db.HOADONs.Add(newHoaDon);
            db.SaveChanges();
            var thongTinPhong = db.PHONGs.ToList().Find(p => p.TENPHONG == TenPhong);
            var dsChiTietHoaDon = db.CTHDs.ToList();
            int maCTHD = 0;

            if (dsChiTietHoaDon.Count != 0)
            {
                maCTHD = dsChiTietHoaDon[dsChiTietHoaDon.Count - 1].MACTHD + 1;
            }

            CTHD newCTHD = new CTHD() { MAPHONG = thongTinPhong.MAPHONG, SONGAYTHUE = SoNgayThue, THANHTIEN = (int)ThanhTien, MACTHD = maCTHD, MAHD = maHD };

            db.CTHDs.Add(newCTHD);
            db.SaveChanges();


            var newPhong = db.PHONGs.ToList().Find(p => p.TENPHONG == TenPhong);
            newPhong.TINHTRANG = "Trống";
            db.PHONGs.Attach(newPhong);
            db.Entry(newPhong).Property(x => x.TINHTRANG).IsModified = true;
            db.SaveChanges();

            var result = MessageBox.Show("Đã lưu thành công!", "Thông báo", MessageBoxButton.OK);
            switch (result)
            {
                case MessageBoxResult.OK:
                    CloseWindow();
                    break;
            }
        }


    }
}
