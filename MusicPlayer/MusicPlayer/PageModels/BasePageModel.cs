using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FreshMvvm;

namespace MusicPlayer.PageModels
{
    public class BasePageModel : FreshBasePageModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual void Initialize() { }
        public virtual void Deinitialize() { }
    }
}
