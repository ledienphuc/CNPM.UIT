using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
        private async void LoadData()
        {
            ObservableCollection<PhongViewModel> _danhSachPhong = new ObservableCollection<PhongViewModel>();
            using (var db = new KhachSanContext())
            {
                var danhMucPhong = await (from p in db.PHONGs
                                          join lp in db.LOAIPHONGs on p.MALOAIPHONG equals lp.MALOAIPHONG
                                          orderby p.MAPHONG
                                          select new { p.TENPHONG, p.LOAIPHONG1, lp.TENLOAIPHONG, lp.DONGIA, p.TINHTRANG, p.GHICHU }).ToListAsync();
                foreach (var phong in danhMucPhong)
                {
                    PhongViewModel phongViewModel = new PhongViewModel { Phong = new PHONG { TENPHONG = phong.TENPHONG, LOAIPHONG1 = phong.LOAIPHONG1, GHICHU = phong.GHICHU, TINHTRANG = phong.TINHTRANG } };
                    _danhSachPhong.Add(phongViewModel);
                }
                DanhMucPhong = _danhSachPhong;
                RaisePropertyChanged("DanhMucPhong");


            }

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

        public DanhSachPhongViewModel()
        {
            LoadData();
            PhongBinding();
            DonGiaBindingLoaiPhong();
            ThemPhongCommand = new RelayCommand<UIElementCollection>(ThemPhong);
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
                    case "txbGhiChu":
                        _ghiChu = a.Text;
                        break;
                    case "cmbLoaiPhong":
                        _tenLoaiPhong = a.Text;
                        break;
                    case "txbDonGia":
                        _donGia = DonGia;
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
        public ICommand ThemPhongCommand { get; set; }
    }

}
