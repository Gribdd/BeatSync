using BeatSync.Pages;
using BeatSync.Services;
using BeatSync.ViewModel.Admin;
using Microsoft.Extensions.Logging;

namespace BeatSync
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Jua-Regular.ttf", "JuaRegular");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //View
            builder.Services.AddTransient<AddArtist>();
            builder.Services.AddTransient<ArtistManagement>();
            builder.Services.AddTransient<AddSong>();
            builder.Services.AddTransient<SongManagement>();


            //Viewmodel
            builder.Services.AddTransient<AddArtistViewModel>();
            builder.Services.AddTransient<ArtistManagementViewModel>();
            builder.Services.AddTransient<AddSongViewModel>();
            builder.Services.AddTransient<SongManagementViewModel>();
            

            //Service
            builder.Services.AddTransient<AdminService>();

            return builder.Build();
        }
    }
}
