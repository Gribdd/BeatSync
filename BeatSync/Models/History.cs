
namespace BeatSync.Models
{
    public partial class History : BaseModel
    { 
        
        [ObservableProperty]
        private int _userId; //User Id

        [ObservableProperty]
        private DateTime _timeStamp; //DateTime of recently played song

        [ObservableProperty]
        private int _songId; //Name of the song

        [ObservableProperty]
        private int _accountType;

        [ObservableProperty]
        private string? _songName;
    }
}
