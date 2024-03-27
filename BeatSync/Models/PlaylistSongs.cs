using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.Models;

/// <summary>
/// Repository for all playlist and songs
/// </summary>
public partial class PlaylistSongs : ObservableObject
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private int _playlistId;

    [ObservableProperty]
    private int _songId;    
}
