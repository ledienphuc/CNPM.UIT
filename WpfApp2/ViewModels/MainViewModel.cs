using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace WpfApp2.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        //readonly static MainViewModel _mainViewModel = new MainViewModel();
        readonly static DanhSachPhongViewModel _danhSachPhongViewModel = new DanhSachPhongViewModel();
        readonly static DatPhongViewModel _datPhongViewModel = new DatPhongViewModel();

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public ICommand MainViewCommand { get; private set; }
        public ICommand DanhSachPhongViewCommand { get; private set; }
        public ICommand DatPhongViewCommand { get; private set; }

        public MainViewModel()
        {
            CurrentViewModel = null;
            //MainViewCommand = new RelayCommand(() => ExecuteFirstViewCommand());
            DanhSachPhongViewCommand = new RelayCommand(() => ExecuteDanhSachPhongViewCommand());
            DatPhongViewCommand = new RelayCommand(() => ExecuteDatPhongViewCommand());
        }

        private void ExecuteMainViewCommand()
        {
           // CurrentViewModel = MainViewModel._mainViewModel;
        }

        private void ExecuteDanhSachPhongViewCommand()
        {
            CurrentViewModel = MainViewModel._danhSachPhongViewModel;
        }

        private void ExecuteDatPhongViewCommand()
        {
            CurrentViewModel = MainViewModel._datPhongViewModel;
        }
    }
}