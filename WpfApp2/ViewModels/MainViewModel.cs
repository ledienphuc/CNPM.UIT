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

        readonly static DanhSachPhongViewModel _firstViewModel = new DanhSachPhongViewModel();
        //readonly static SecondViewModel _secondViewModel = new SecondViewModel();

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

        public ICommand FirstViewCommand { get; private set; }
        public ICommand SecondViewCommand { get; private set; }

        public MainViewModel()
        {
            //CurrentViewModel = MainViewModel._firstViewModel;
            FirstViewCommand = new RelayCommand(() => ExecuteFirstViewCommand());
            //SecondViewCommand = new RelayCommand(() => ExecuteSecondViewCommand());
        }

        private void ExecuteFirstViewCommand()
        {
            CurrentViewModel = MainViewModel._firstViewModel;
        }

        //private void ExecuteSecondViewCommand()
        //{
        //    CurrentViewModel = MainViewModel._secondViewModel;
        //}
    }
}