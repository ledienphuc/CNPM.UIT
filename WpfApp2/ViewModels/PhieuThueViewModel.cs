using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp2.Services;

namespace WpfApp2.ViewModels
{
    class PhieuThueViewModel : ViewModelBase
    {
        private List<KhachHangViewModel> _danhSachKhachHang = new List<KhachHangViewModel>();


        public DateTime thisDay { get; set; }

        private string _tenPhong;
        public string TenPhong
        {
            get
            {
                return _tenPhong;
            }
            set
            {
                _tenPhong = value;
                RaisePropertyChanged("TenPhong");
            }
        }

        public string LoaiKH1Selected { get; set; }
        public string LoaiKH2Selected { get; set; }
        public string LoaiKH3Selected { get; set; }

        public ObservableCollection<String> DanhSachLoaiKhach { get; set; }

        void LoadDanhSachLoaiKhach()
        {
            KhachSanContext db = new KhachSanContext();
            ObservableCollection<String> danhSachLoaiKhach = new ObservableCollection<string>();
            var danhSachKhachhang = db.LOAIKHACHes.ToList();
            foreach (var khachHang in danhSachKhachhang)
            {
                danhSachLoaiKhach.Add(khachHang.TENLOAIKHACH);
            }

            DanhSachLoaiKhach = danhSachLoaiKhach;

        }


        public PhieuThueViewModel(string tenPhong)
        {
            TenPhong = tenPhong;
            thisDay = DateTime.Today;
            LoadDanhSachLoaiKhach();
            LuuCommand = new RelayCommand<UIElementCollection>(luuKhachHang);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }

        #region Command

        public ICommand LuuCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        #endregion
        /// <summary>
        /// Ham excute cho LuuCommand dung de luu thong tin KH vao CSDL 
        /// </summary>
        /// <param name="UI"> List cac UI con de lay thong tin KH</param>
        void luuKhachHang(UIElementCollection UI)
        {
            int count = 0;
            
            KhachHangViewModel KH1 = new KhachHangViewModel();
            KhachHangViewModel KH2 = new KhachHangViewModel();
            KhachHangViewModel KH3 = new KhachHangViewModel();

            foreach (var item in UI)
            {
                TextBox a = item as TextBox;
                if (a == null) continue;
                switch (a.Name)
                {
                    case "KH1":
                        if (a.Text != "")
                        {
                            count++;
                            KH1.TenKhachHang = a.Text;
                        }
                        break;
                    case "KH2":
                        if (a.Text != "")
                        {
                            count++;
                            KH2.TenKhachHang = a.Text;
                        }
                        break;
                    case "KH3":
                        if (a.Text != "")
                        {
                            count++;
                            KH3.TenKhachHang = a.Text;
                        }
                        break;
                    case "CMNDKH1":
                        KH1.CMND = a.Text;
                        break;
                    case "CMNDKH2":
                        KH2.CMND = a.Text;
                        break;
                    case "CMNDKH3":
                        KH3.CMND = a.Text;
                        break;
                    case "DiaChiKH1":
                        KH1.DiaChi = a.Text;
                        break;
                    case "DiaChiKH2":
                        KH2.DiaChi = a.Text;
                        break;
                    case "DiaChiKH3":
                        KH3.DiaChi = a.Text;
                        break;
                }

            }

            if (KH1.TenKhachHang != null) _danhSachKhachHang.Add(KH1);
            if (KH2.TenKhachHang != null) _danhSachKhachHang.Add(KH2);
            if (KH3.TenKhachHang != null) _danhSachKhachHang.Add(KH3);

            KhachSanContext db = new KhachSanContext();
            int maKH = 0;
            var dsKH = db.KHACHHANGs.ToList();
            if (dsKH.Count != 0)
            {
                
                maKH = dsKH[dsKH.Count - 1].MAKHACHHANG + 1;
            }

            // lay danh sach ma loai khach
            List<int> dSMaLK = new List<int>();
            if (LoaiKH1Selected != null)
                dSMaLK.Add(db.LOAIKHACHes.ToList().Find(p => p.TENLOAIKHACH == LoaiKH1Selected).MALOAIKHACH);
            if (LoaiKH2Selected != null)
                dSMaLK.Add(db.LOAIKHACHes.ToList().Find(p => p.TENLOAIKHACH == LoaiKH2Selected).MALOAIKHACH);
            if (LoaiKH3Selected != null)
                dSMaLK.Add(db.LOAIKHACHes.ToList().Find(p => p.TENLOAIKHACH == LoaiKH3Selected).MALOAIKHACH);

            // Sua tinh trang phong 
            int maPhong = db.PHONGs.ToList().Find(p => p.TENPHONG == TenPhong).MAPHONG;
            var newPhong = db.PHONGs.ToList().Find(p => p.TENPHONG == TenPhong);
            newPhong.TINHTRANG = "Đầy";
            db.PHONGs.Attach(newPhong);
            db.Entry(newPhong).Property(x => x.TINHTRANG).IsModified = true;
            db.SaveChanges();
            List<KHACHHANG> danhSachKhachHang = new List<KHACHHANG>();
            // Luu khach hang
            for (int i = 0; i < dSMaLK.Count; i++)
            {
                KHACHHANG newKH = new KHACHHANG()
                {
                    TENKHACHHANG = _danhSachKhachHang[i].TenKhachHang,
                    CMND = _danhSachKhachHang[i].CMND,
                    DIACHI = _danhSachKhachHang[i].DiaChi,
                    MAKHACHHANG = maKH,
                    MALOAIKHACH = dSMaLK[i],
                    MAPHONG = maPhong,
                };
                danhSachKhachHang.Add(newKH);
                db.KHACHHANGs.Add(newKH);
                maKH++;
            }
            db.SaveChanges();

            //Luu Phieu Thue
            int maPhieuThue = 0;
            var dsPhieuThue = db.PHIEUTHUEs.ToList();

            if(dsPhieuThue.Count != 0)
            {
                maPhieuThue = dsPhieuThue[dsPhieuThue.Count-1].MAPHIEUTHUE + 1;
            }

            PHIEUTHUE newPhieuThue = new PHIEUTHUE() { MAPHONG = maPhong, NGAYTHUE = thisDay, SOLUONGKHACH = _danhSachKhachHang.Count, MAPHIEUTHUE = maPhieuThue };

            db.PHIEUTHUEs.Add(newPhieuThue);
            db.SaveChanges();


            //Luu CTPT
            var dsCTPT = db.CTPTs.ToList();
            int maCTPT = 0;
            if (dsCTPT.Count != 0)
            {
                maCTPT = dsCTPT[dsCTPT.Count - 1].MACTPT + 1;
            }
            foreach(var kh in danhSachKhachHang)
            {
                db.CTPTs.Add(new CTPT() { MAKHACHHANG = kh.MAKHACHHANG , MAPHIEUTHUE = maPhieuThue, MACTPT = maCTPT });
                maCTPT++;
            }
            db.SaveChanges();

            var result = MessageBox.Show("Đã lưu thành công!", "Thông báo", MessageBoxButton.OK);
            switch (result)
            {
                case MessageBoxResult.OK:
                    CloseWindow();
                    break;
            }
            
        }

        void CloseWindow()
        {
            Messenger.Default.Send<OpenWindowMessage>(new OpenWindowMessage() { WindowName = View.PhieuThuePhong, Message = "Close Window"});
        }

    }
}
