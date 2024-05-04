
namespace BeatSync.Models;

public partial class Likes : BaseModel
{
    [ObservableProperty]
    private int _userID;

    [ObservableProperty]
    private int _songID;

    public DateTime CreatedAt { get; set; }
}
