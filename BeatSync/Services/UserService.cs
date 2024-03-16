
using BeatSync.Models;
using BeatSync.Views;
using CommunityToolkit.Mvvm.ComponentModel;
namespace BeatSync.Services;

public class UserService : ObservableObject
{
    private readonly UserValidationService _userValidationService;


    public UserService(UserValidationService userValidationService)
    {
        _userValidationService = userValidationService;
    }


    public async Task<bool> Authenticate(string identifier, string password)
    {
        if(!await _userValidationService.DoesUserExist(identifier))
        {
            return false;
        }

        var user = GetUser(identifier, password);

        if(user != null)
        {
            NavigateBasedOnUserType(user);
            return true;
        }
        
        return false;
    }

    private void NavigateBasedOnUserType(User user)
    {
        switch(user.AccounType)
        {
            case 1:
                Application.Current!.MainPage = new PublisherLandingPage();
                break;
            case 2:
                Application.Current!.MainPage = new PublisherLandingPage();
                break;
            case 3:
                Application.Current!.MainPage = new CustomerLandingPage();
                break;
            default:
                break;
        }
    }

    private User? GetUser(string identifier, string password)
    {
        var artists = _userValidationService.GetArtists();
        var artist = artists.FirstOrDefault(a => (a.Username == identifier || a.Email == identifier) && (a.Password == password));
        if (artist != null)
        {
            return new User
            {
                Id = artist.Id,
                Email = artist.Email,
                Username = artist.Username,
                Password = artist.Password,
                DateOfBirth = artist.DateOfBirth,
                FirstName = artist.FirstName,
                LastName = artist.LastName,
                Gender = artist.Gender,
                AccounType = artist.AccounType,
                IsDeleted = artist.IsDeleted,
                ImageFilePath = artist.ImageFilePath
            };
        }

        var publishers = _userValidationService.GetPublishers();
        var publisher = publishers.FirstOrDefault(p => (p.Username == identifier || p.Email == identifier) && (p.Password == password));
        if (publisher != null)
        {
            return new User
            {
                Id = publisher.Id,
                Email = publisher.Email,
                Username = publisher.Username,
                Password = publisher.Password,
                DateOfBirth = publisher.DateOfBirth,
                FirstName = publisher.FirstName,
                LastName = publisher.LastName,
                Gender = publisher.Gender,
                AccounType = publisher.AccounType,
                IsDeleted = publisher.IsDeleted,
                ImageFilePath = publisher.ImageFilePath
            };
        }

        var users = _userValidationService.GetUsers();
        var user = users.FirstOrDefault(u => (u.Username == identifier || u.Email == identifier) && (u.Password == password));
        return user;
    }   
}
