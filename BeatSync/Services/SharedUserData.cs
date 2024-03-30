

namespace BeatSync.Services;

public class SharedUserData : ISharedUserData
{
    public Publisher Publisher { get; set; } = new();
}
