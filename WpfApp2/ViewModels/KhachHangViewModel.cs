using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.ViewModels
{
    class KhachHangViewModel : ViewModelBase
    {
        public string TenKhachHang { get; set; }

        public string LoaiKhach { get; set; }

        public string CMND { get; set; }

        public string DiaChi { get; set; }
    }
}
