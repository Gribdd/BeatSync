using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BeatSync.Repositories.IRepository;
using BeatSync.Services.IService;
using BeatSync.ViewModel.ArtistShell;

namespace BeatSync
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Jua-Regular.ttf", "JuaRegular");
                })
                .RegisterServices()
                .RegisterRepositories()
                .RegisterViewModels()
                .RegisterViews();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();

        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<UserService>();
            mauiAppBuilder.Services.AddTransient<PublisherService>();
            mauiAppBuilder.Services.AddTransient<ArtistService>();
            mauiAppBuilder.Services.AddTransient<SongService>();
            mauiAppBuilder.Services.AddTransient<UserAuthService>();
            mauiAppBuilder.Services.AddTransient<UserValidationService>();
            mauiAppBuilder.Services.AddTransient<AlbumService>();
            mauiAppBuilder.Services.AddTransient<PlaylistService>();
            mauiAppBuilder.Services.AddTransient<PlaylistSongService>();
            mauiAppBuilder.Services.AddTransient<FileUploadService>();
            mauiAppBuilder.Services.AddTransient<HistoryService>();
            mauiAppBuilder.Services.AddTransient<MyCollection>();
            mauiAppBuilder.Services.AddTransient<LikesService>();
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterRepositories(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<IUnitofWork, UnitOfWork>();
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            //Viewmodel
            mauiAppBuilder.Services.AddTransient<AddArtistViewModel>();
            mauiAppBuilder.Services.AddTransient<ArtistManagementViewModel>();
            mauiAppBuilder.Services.AddTransient<SongManagementViewModel>();
            mauiAppBuilder.Services.AddTransient<PublisherManagementViewModel>();
            mauiAppBuilder.Services.AddTransient<AddPublisherViewModel>();
            mauiAppBuilder.Services.AddTransient<AddUserViewModel>();
            mauiAppBuilder.Services.AddTransient<UserManagementViewModel>();
            mauiAppBuilder.Services.AddTransient<AdminSearchPageViewModel>();

            //Viewmodel registration and Login
            mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
            mauiAppBuilder.Services.AddTransient<SignUpPageViewModel>();
            mauiAppBuilder.Services.AddTransient<LoginPageViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountUsernameViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountPasswordViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountDOBViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountFirstNameViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountLastNameViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountGenderViewModel>();
            mauiAppBuilder.Services.AddTransient<CreateAccountUploadImageViewModel>();

            //General
            mauiAppBuilder.Services.AddTransient<AddAlbumSongsViewModel>();
            mauiAppBuilder.Services.AddTransient<AddSongViewModel>();
            mauiAppBuilder.Services.AddTransient<ViewProfileViewModel>();

            //Publisher
            mauiAppBuilder.Services.AddTransient<SongManagementPubViewModel>();
            mauiAppBuilder.Services.AddTransient<LibraryPageViewModel>();
            mauiAppBuilder.Services.AddTransient<AddAlbumPublisherViewModel>();
            mauiAppBuilder.Services.AddTransient<PubUserHistoryViewModel>();
            mauiAppBuilder.Services.AddTransient<PubUserHistoryViewModel>();
            mauiAppBuilder.Services.AddTransient<PublisherLandingPageViewModel>();
            mauiAppBuilder.Services.AddTransient<PubRecentlyPlayedViewModel>();

            //Users
            mauiAppBuilder.Services.AddTransient<SearchPageViewModel>();
            mauiAppBuilder.Services.AddTransient<LandingPageViewModel>();
            mauiAppBuilder.Services.AddTransient<CustomerLibraryPageViewModel>();
            mauiAppBuilder.Services.AddTransient<AddPlaylistCustomerViewModel>();
            mauiAppBuilder.Services.AddTransient<AddPlaylistSongsCustomerViewModel>();
            mauiAppBuilder.Services.AddTransient<AddPlaylistSongsSearchViewModel>();
            mauiAppBuilder.Services.AddTransient<CustomerLandingPageViewModel>();
            mauiAppBuilder.Services.AddTransient<CustomerRecentlyPlayedViewModel>();
            mauiAppBuilder.Services.AddTransient<CustomerFavoriteSongsViewModel>();

            //Artist
            mauiAppBuilder.Services.AddTransient<ArtistLandingPageViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            //View
            mauiAppBuilder.Services.AddTransient<AddArtist>();
            mauiAppBuilder.Services.AddTransient<ArtistManagement>();
            mauiAppBuilder.Services.AddTransient<SongManagement>();
            mauiAppBuilder.Services.AddTransient<PublisherManagement>();
            mauiAppBuilder.Services.AddTransient<AddPublisher>();
            mauiAppBuilder.Services.AddTransient<PubUserHistory>();
            mauiAppBuilder.Services.AddTransient<Admin_LandingPage>();
            mauiAppBuilder.Services.AddTransient<Admin_LoginPage>();
            mauiAppBuilder.Services.AddTransient<MainPage>();
            mauiAppBuilder.Services.AddTransient<AddUser>();
            mauiAppBuilder.Services.AddTransient<UserManagement>();
            mauiAppBuilder.Services.AddTransient<AdminSearchPage>();

            //View Signup and Login
            mauiAppBuilder.Services.AddTransient<LoginPage>();
            mauiAppBuilder.Services.AddTransient<SignUpPage>();
            mauiAppBuilder.Services.AddTransient<CreateAccountUsername>();
            mauiAppBuilder.Services.AddTransient<CreateAccountPassword>();
            mauiAppBuilder.Services.AddTransient<CreateAccountDOB>();
            mauiAppBuilder.Services.AddTransient<CreateAccountFirstName>();
            mauiAppBuilder.Services.AddTransient<CreateAccountLastName>();
            mauiAppBuilder.Services.AddTransient<CreateAccountGender>();
            mauiAppBuilder.Services.AddTransient<CreateAccountUploadImage>();
            mauiAppBuilder.Services.AddTransient<CustomerLandingPage>();
            mauiAppBuilder.Services.AddTransient<PublisherLandingPage>();

            //General, meaning can be used more than once
            mauiAppBuilder.Services.AddTransient<AddAlbumSongs>();
            mauiAppBuilder.Services.AddTransient<AddSong>();
            mauiAppBuilder.Services.AddTransient<ViewProfile>();


            //Publisher
            mauiAppBuilder.Services.AddTransient<LibraryPage>();
            mauiAppBuilder.Services.AddTransient<SongManagementPub>();
            mauiAppBuilder.Services.AddTransient<AddAlbumPublisher>();
            mauiAppBuilder.Services.AddTransient<PubRecentlyPlayed>();
            mauiAppBuilder.Services.AddTransient<PubViewProfile>();

            //Users
            mauiAppBuilder.Services.AddTransient<SearchPage>();
            mauiAppBuilder.Services.AddTransient<LandingPage>();
            mauiAppBuilder.Services.AddTransient<CustomerLibraryPage>();
            mauiAppBuilder.Services.AddTransient<AddPlaylistCustomer>();
            mauiAppBuilder.Services.AddTransient<AddPlaylistSongsCustomer>();
            mauiAppBuilder.Services.AddTransient<AddPlaylistSongsSearch>();
            mauiAppBuilder.Services.AddTransient<CustomerRecentlyPlayed>();
            mauiAppBuilder.Services.AddTransient<CustomerFavoriteSongs>();


            return mauiAppBuilder;
        }
    }
}
