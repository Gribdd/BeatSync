namespace BeatSync.Controls;

public partial class CardView : Frame
{
    public CardView()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty CardTitleProperty =
        BindableProperty.Create(nameof(CardTitle), typeof(string), typeof(CardView), string.Empty);

    public static readonly BindableProperty CardImageProperty =
        BindableProperty.Create(nameof(CardImage), typeof(string), typeof(CardView), string.Empty);

    public static readonly BindableProperty CardSubtitleLeftProperty =
        BindableProperty.Create(nameof(CardSubtitleLeft), typeof(string), typeof(CardView), string.Empty);

    public static readonly BindableProperty CardSubtitleRightProperty =
        BindableProperty.Create(nameof(CardSubtitleRight), typeof(string), typeof(CardView), string.Empty);

    public string CardTitle
    {
        get => (string)GetValue(CardTitleProperty);
        set => SetValue(CardTitleProperty, value);
    }

    public string CardImage
    {
        get => (string)GetValue(CardImageProperty);
        set => SetValue(CardImageProperty, value);
    }

    public string CardSubtitleLeft
    {
        get => (string)GetValue(CardSubtitleLeftProperty);
        set => SetValue(CardSubtitleLeftProperty, value);
    }
    public string CardSubtitleRight
    {
        get => (string)GetValue(CardSubtitleRightProperty);
        set => SetValue(CardSubtitleRightProperty, value);
    }



}
