using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using WPF_autoparking.Views;

namespace WPF_autoparking.ViewModel
{
    internal class MainViewModel : ViewModedBase
    {
        private Page _CurPage = new CarPage();
        private Page _carPage = new CarPage();
        private Page _paymentPage = new PaymentPage();
        private Page _carDamagePage = new CarDamagePage();
        private Page _topRentalPage = new TopRentalPage();

        public Page CurPage
        {
            get => _CurPage;
            set => Set(ref _CurPage, value);
        }

        public ICommand CarPage
        {
            get
            {
                return new RelayCommand(() => CurPage = _carPage);
            }
        }
        public ICommand PaymentPage
        {
            get
            {
                return new RelayCommand(() => CurPage = _paymentPage);
            }
        }
        public ICommand CarDamagePage
        {
            get
            {
                return new RelayCommand(() => CurPage = _carDamagePage);
            }
        }
        public ICommand TopRentalPage
        {
            get
            {
                return new RelayCommand(() => CurPage = _topRentalPage);
            }
        }
    }
}
