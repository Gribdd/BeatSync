namespace BeatSync.Models
{
    public partial class History : ObservableObject
    {
        [ObservableProperty]
        private int _id; //History Id

        [ObservableProperty]
        private int _userId; //User Id

        [ObservableProperty]
        private DateTime _timeStamp; //DateTime of recently played song

        [ObservableProperty]
        private ObservableCollection<Song> _song;

    }
}
