namespace BeatSync.Controls;

public partial class MediaPlayerControl : Frame, INotifyPropertyChanged
{
    private const string PlayIcon = "play.png";
    private const string PauseIcon = "pause.png";
    private bool isIconInitialized = false;

    public MediaPlayerControl()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(MediaPlayerControl), string.Empty);

    public static readonly BindableProperty SongNameProperty =
        BindableProperty.Create(nameof(SongName), typeof(string), typeof(MediaPlayerControl), string.Empty);

    public static readonly BindableProperty ArtistNameProperty =
        BindableProperty.Create(nameof(ArtistName), typeof(string), typeof(MediaPlayerControl), string.Empty);

    public static readonly BindableProperty AudioSourceProperty =
        BindableProperty.Create(nameof(AudioSource), typeof(MediaSource), typeof(MediaPlayerControl));

    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public string SongName
    {
        get => (string)GetValue(SongNameProperty);
        set => SetValue(SongNameProperty, value);
    }

    public string ArtistName
    {
        get => (string)GetValue(ArtistNameProperty);
        set => SetValue(ArtistNameProperty, value);
    }

    public MediaSource AudioSource
    {
        get => (MediaSource)GetValue(AudioSourceProperty);
        set => SetValue(AudioSourceProperty, value);
    }

    private void OnButtonPlayClicked(object sender, EventArgs e)
    {
        if (!isIconInitialized)
        {
            mediaPlayer.Play();
            MediaPlayerState = PauseIcon;
            isIconInitialized = true;
            return;
        }

        if (mediaPlayer.CurrentState == MediaElementState.Stopped ||
            mediaPlayer.CurrentState == MediaElementState.Paused)
        {
            mediaPlayer.Play();
        }
        else if (mediaPlayer.CurrentState == MediaElementState.Playing)
        {
            mediaPlayer.Pause();
        }

        MediaPlayerState = mediaPlayer.CurrentState == MediaElementState.Stopped || mediaPlayer.CurrentState == MediaElementState.Paused ? PlayIcon : PauseIcon;
    }

    private string? mediaPlayerState = PlayIcon;
    public string? MediaPlayerState
    {
        get => mediaPlayerState; 
        set
        {
            mediaPlayerState = value;
            OnPropertyChanged("MediaPlayerState");
        }
    }
}