using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2.ViewModels
{
    class DanhSachPhongViewModel : ViewModelBase
    {

        private PHONG selectedPhong;
        public PHONG SelectedPhong
        {
            get { return selectedPhong; }
            set
            {
                selectedPhong = value;
                RaisePropertyChanged("SelectedPhong");
            }
        }



        public ObservableCollection<PhongViewModel> DanhMucPhong { get; set; }
        public async void LoadData()
        {
            ObservableCollection<PhongViewModel> _danhSachPhong = new ObservableCollection<PhongViewModel>();
            using (var db = new KhachSanContext())
            {
                var danhMucPhong = await (from p in db.PHONGs
                                          join lp in db.LOAIPHONGs on p.MALOAIPHONG equals lp.MALOAIPHONG
                                          orderby p.MAPHONG
                                          select new { p.TENPHONG, p.LOAIPHONG, lp.TENLOAIPHONG, lp.DONGIA, p.TINHTRANG, p.GHICHU,p.MAPHONG,p.MALOAIPHONG }).ToListAsync();
                foreach (var phong in danhMucPhong)
                {
                    PhongViewModel phongViewModel = new PhongViewModel { Phong = new PHONG { TENPHONG = phong.TENPHONG, LOAIPHONG = phong.LOAIPHONG, GHICHU = phong.GHICHU, TINHTRANG = phong.TINHTRANG,MAPHONG = phong.MAPHONG,MALOAIPHONG = phong.MALOAIPHONG } };
                    _danhSachPhong.Add(phongViewModel);
                }
                DanhMucPhong = _danhSachPhong;
                RaisePropertyChanged("DanhMucPhong");


            }

        }

        public int LienKetMaLoaiPhong()
        {
            int _maLoaiPhong = 0;
            KhachSanContext db = new KhachSanContext();
            var dsLoaiPhong = db.LOAIPHONGs.ToList<LOAIPHONG>();
            foreach(var _loaiphong in dsLoaiPhong)
            {
                if(_loaiphong.TENLOAIPHONG == loaiPhongSelected)
                {
                    _maLoaiPhong = _loaiphong.MALOAIPHONG;
                }
            }
            return _maLoaiPhong;
        }
        public ObservableCollection<String> DanhSachLoaiPhong { get; set; }
        public void PhongBinding()
        {
            KhachSanContext db = new KhachSanContext();
            var dsLoaiPhong = db.LOAIPHONGs.ToList<LOAIPHONG>();


            ObservableCollection<String> _danhSachLoaiPhong = new ObservableCollection<String>();
            foreach (var loaiPhong in dsLoaiPhong)
            {
                _danhSachLoaiPhong.Add(loaiPhong.TENLOAIPHONG);
            }
            DanhSachLoaiPhong = _danhSachLoaiPhong;
            DanhSachLoaiPhong = new ObservableCollection<string>(DanhSachLoaiPhong.Distinct());

        }

        
        public ObservableCollection<LoaiPhongViewModel> LoaiPhongUngVoiDonGia { get; set; }
        public async void DonGiaBindingLoaiPhong()
        {
            ObservableCollection<LoaiPhongViewModel> _LoaiPhongUngVoiDonGia = new ObservableCollection<LoaiPhongViewModel>();
            using (var db = new KhachSanContext())
            {
                var danhSachLoaiPhong = await (
                                               from lp in db.LOAIPHONGs 
                                               select new { lp.DONGIA,lp.TENLOAIPHONG }).ToListAsync();


                foreach (var phong in danhSachLoaiPhong)
                {
                    LoaiPhongViewModel loaiPhongViewModel = new LoaiPhongViewModel { LoaiPhong = new LOAIPHONG { TENLOAIPHONG = phong.TENLOAIPHONG, DONGIA = phong.DONGIA } };
                    _LoaiPhongUngVoiDonGia.Add(loaiPhongViewModel);

                }

                LoaiPhongUngVoiDonGia = _LoaiPhongUngVoiDonGia;

            }

           
    }
        int donGia;
        public int DonGia
        {
            get
            {
                return donGia;
            }
            set
            {
                donGia = value;
                RaisePropertyChanged("DonGia");
            }
        }
        string loaiPhongSelected;
        public string LoaiPhongSelected
        {
            get
            {
                foreach ( var loaiphong in LoaiPhongUngVoiDonGia )
                {
                    if (loaiPhongSelected == loaiphong.TenLoaiPhong)
                        DonGia = loaiphong.DonGia;
                }

                
                return loaiPhongSelected;
            }
            set
            {

                    loaiPhongSelected = value;
            }
        }
        private PhongViewModel phongSelected;

        public PhongViewModel PhongSelected
        {
            get { return phongSelected; }
            set { phongSelected = value; }
        }

        public DanhSachPhongViewModel()
        {
            LoadData();
            PhongBinding();
            DonGiaBindingLoaiPhong();
            ThemPhongCommand = new RelayCommand<UIElementCollection>(ThemPhong);
            XoaPhongCommand = new RelayCommand(XoaPhong);
            SuaPhongCommand = new RelayCommand<UIElementCollection>(SuaPhong);
            Messenger.Default.Register<OpenWindowMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(OpenWindowMessage msg)
        {
            if (msg.Message == "Close Window")
            {
                LoadData();
            }
        }

        private void ThemPhong(UIElementCollection UI)
        {
            KhachSanContext db = new KhachSanContext();

            string _tenPhong = "";
            string _ghiChu = "";
            string _tenLoaiPhong = "";
            int _donGia = 0;

            foreach (var item in UI)
            {
                TextBox a = item as TextBox;
                if (a == null) continue;
                switch (a.Name)
                {
                    case "txbTenPhong":
                        _tenPhong = a.Text;
                        break;
 
                
                }

                _donGia = DonGia;
                _tenLoaiPhong = LoaiPhongSelected;
               
            }

            var dsPhong = db.PHONGs.ToList<PHONG>();
            int newMaPhong = 0;
            if(dsPhong.Count == 0) { newMaPhong = 0; }
            else 
            newMaPhong = dsPhong[dsPhong.Count-1].MAPHONG+1;
            PHONG newPhong = new PHONG()
            {

                TENPHONG = _tenPhong,
                MAPHONG = newMaPhong,
                GHICHU = "Không có",
                TINHTRANG = "Trống",
                
            };

            var newLoaiPhong = db.LOAIPHONGs.ToList<LOAIPHONG>().Find((p)=>p.TENLOAIPHONG == LoaiPhongSelected);
            int maPhong = newLoaiPhong.MALOAIPHONG;
            newPhong.MALOAIPHONG = maPhong;



            //db.Entry(newPhong).State = System.Data.Entity.EntityState.Added;
            db.PHONGs.Add(newPhong);
           // db.LOAIPHONGs.Add(newLoaiPhong);

            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.Write("Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    { 
                        Debug.Write("- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage);
                    }
                }
                throw;
            }
            LoadData();


        }

        private void XoaPhong()
        {
            KhachSanContext db = new KhachSanContext();

            var phongdeleted = new PHONG { MAPHONG = phongSelected.MaPhong };
            db.PHONGs.Attach(phongdeleted);
            db.PHONGs.Remove(phongdeleted);
            db.SaveChanges();
            LoadData();
        }


        private void SuaPhong(UIElementCollection UI)
        {
            KhachSanContext db = new KhachSanContext();

            string _tenPhong = "";
            string _tenLoaiPhong = loaiPhongSelected;
            string _ghiChu = "";

            foreach (var item in UI)
            {
                TextBox a = item as TextBox;
                if (a == null) continue;
                switch (a.Name)
                {
                    case "txbTenPhong":
                        _tenPhong = a.Text;
                        break;
                    case "txbGhiChu":
                        _ghiChu = a.Text;
                        break;                
                }

            }

            PHONG updatedPhong = new PHONG { TENPHONG = _tenPhong, MAPHONG = PhongSelected.MaPhong, GHICHU = _ghiChu, TINHTRANG = phongSelected.TinhTrang, MALOAIPHONG = LienKetMaLoaiPhong() };

            PHONG original = db.PHONGs.Find(updatedPhong.MAPHONG);

            if (original != null)
            {
                db.Entry(original).CurrentValues.SetValues(updatedPhong);
                db.SaveChanges();
            }
            LoadData();
        }


        //private void SuaPhong() { };
        public ICommand ThemPhongCommand { get; set; }
        public ICommand XoaPhongCommand { get; set; }
        public ICommand SuaPhongCommand { get; set; }
     
    }

}
;