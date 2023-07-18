using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace DocsMarshal.MVVM.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private BaseViewModel()
        {

        }

        public BaseViewModel(Page curPage, INavigation navigation)
        {

            CurPage = curPage;
            Navigation = navigation;
        }

        bool _IsRunning = false;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        public INavigation Navigation { get; private set; }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        string _Error = String.Empty;
        public string Error
        {
            get
            {
                return _Error;
            }

            set
            {
                if (!string.Equals(_Error, value, StringComparison.OrdinalIgnoreCase))
                {
                    _Error = value;
                    OnPropertyChanged();
                }
            }
        }

        public Page CurPage { get; private set; }
    }
}

