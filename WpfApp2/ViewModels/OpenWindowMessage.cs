using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.ViewModels
{
    class OpenWindowMessage
    {
        public View WindowName { get; set; }
        public string TenPhong { get; set; }
        public List<KhachHangViewModel> DanhSachKhachHang { get; set; }
        public PhongViewModel Phong { get; set; }
    }

    enum View
    {
        PhieuThuePhong, HoaDon
    }
}
