using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.ViewModels
{
    class DanhSachPhongViewModel : ViewModelBase
    {
        public ObservableCollection<PhongViewModel> DanhMucPhong { get; set; }
        private async void LoadData()
        {
             ObservableCollection<PhongViewModel> _danhSachPhong = new ObservableCollection<PhongViewModel>();
            using (var db = new QUANLYDATPHONGEntities())
            {
                var danhMucPhong = await (from p in db.PHONGs
                                          join lp in db.LOAIPHONGs on p.MALOAIPHONG equals lp.MALOAIPHONG
                                          orderby p.MAPHONG
                                          select new { p.TENPHONG,p.LOAIPHONG, lp.TENLOAIPHONG,  lp.DONGIA, p.TINHTRANG, p.GHICHU }).ToListAsync();
                foreach ( var phong in danhMucPhong)
                {
                    PhongViewModel phongViewModel = new PhongViewModel { Phong = new PHONG { TENPHONG = phong.TENPHONG, LOAIPHONG = phong.LOAIPHONG, GHICHU = phong.GHICHU, TINHTRANG = phong.TINHTRANG } };
                    _danhSachPhong.Add(phongViewModel);
                }
                DanhMucPhong = _danhSachPhong;
                RaisePropertyChanged("DanhMucPhong");
            }

        }

        public DanhSachPhongViewModel()
        {
            LoadData();

        }
    }
           
}
