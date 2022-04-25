using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModelView
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler 
            PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
