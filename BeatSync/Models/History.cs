using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeatSync.Models
{
    public partial class History : ObservableObject
    { //model
        [ObservableProperty]
        private int _id; //History Id

        [ObservableProperty]
        private int _userId; //User Id

        [ObservableProperty]
        private DateTime _timeStamp; //DateTime of recently played song

        [ObservableProperty]
        private string? _songName; //Name of the song

    }
}
