﻿using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp2.ViewModels;
namespace WpfApp2.Views
{
    /// <summary>
    /// Interaction logic for PhieuThuePhong.xaml
    /// </summary>
    public partial class PhieuThuePhong : Window
    {
        public PhieuThuePhong()
        {  
            InitializeComponent();
            Messenger.Default.Register<OpenWindowMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(OpenWindowMessage msg)
        {
            if(msg.WindowName == View.PhieuThuePhong && msg.Message == "Close Window")
            {
                this.Close();
            }
        }
    }
}
