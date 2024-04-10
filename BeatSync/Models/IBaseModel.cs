namespace BeatSync.Models;

public interface IBaseModel
{
    int Id { get; set; }
    bool IsDeleted { get; set; }
}

