using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.ViewModels
{
    class LoaiPhongViewModel : ViewModelBase
    {
        LOAIPHONG _loaiPhong;


        public LOAIPHONG LoaiPhong
        {
            get
            {
                return _loaiPhong;
            }

            set
            {
                _loaiPhong = value;
            }
        }
        public string TenLoaiPhong
        {
            get
            {
                return _loaiPhong.TENLOAIPHONG;
            }
            set
            {
                _loaiPhong.TENLOAIPHONG = value;
                RaisePropertyChanged("TenLoaiPhong");
            }
        }

        public int DonGia {
            get { return _loaiPhong.DONGIA; }
            set {
                _loaiPhong.DONGIA = value;
                RaisePropertyChanged("DonGia");
            }
        }
    }
}
