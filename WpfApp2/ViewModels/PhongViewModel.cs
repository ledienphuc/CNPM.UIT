using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2.Services;

namespace WpfApp2.ViewModels
{
    class PhongViewModel : ViewModelBase, IWindowService
    {
        PHONG _phong;

        public ICommand ShowWindow { get; set; }

        public PhongViewModel()
        {
            ShowWindow = new RelayCommand(() =>
            {
                showWindow(new PhieuThueViewModel());
            });
        }
        public PHONG Phong
        {
            get
            {
                return _phong;
            }

            set
            {
                _phong = value;
            }
        }

        public string TenPhong
        {
            get { return _phong.TENPHONG; }
            set { _phong.TENPHONG = value; RaisePropertyChanged("TenPhong"); }
        }

        public string LoaiPhong
        {
            get { return _phong.LOAIPHONG.TENLOAIPHONG; }
            set { _phong.LOAIPHONG.TENLOAIPHONG = value; RaisePropertyChanged("LoaiPhong"); }
        }

        public Int32 DonGia
        {
            get { return _phong.LOAIPHONG.DONGIA; }
            set { _phong.LOAIPHONG.DONGIA = value; RaisePropertyChanged("DonGia"); }
        }

        public string TinhTrang
        {
            get { return _phong.TINHTRANG; }
            set { _phong.TINHTRANG = value; RaisePropertyChanged("TinhTrang"); }
        }

        public string GhiChu
        {
            get { return _phong.GHICHU; }
            set { _phong.GHICHU = value; RaisePropertyChanged("GhiChu"); }
        }

        public void showWindow(object viewModel)
        {
            var win = new Window();
            win.Content = viewModel;
            win.Show();
        }
    }
}
