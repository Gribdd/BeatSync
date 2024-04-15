
namespace BeatSync.Models;

public partial class BaseModel : ObservableObject, IBaseModel
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private bool _isDeleted;

    public BaseModel()
    {
        IsDeleted = false;
    }
}
