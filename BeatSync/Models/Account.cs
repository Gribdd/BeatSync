
namespace BeatSync.Models;

/// <summary>
/// an account for the user
/// validates the user's input for registration
/// uses tuple to return a boolean and a string message that is displayed to the user via an alert
/// </summary>
public partial class Account : BaseModel
{
    [ObservableProperty]
    private string? _email;

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private DateTime _dateOfBirth;

    [ObservableProperty]
    private string? _firstName;

    [ObservableProperty]
    private string? _lastName;

    [ObservableProperty]
    private string? _gender;

    [ObservableProperty]
    private string? _imageFilePath;

    [ObservableProperty]
    private int _accountType;

    public string? FullName => $"{FirstName} {LastName}";

    public Account()
    {
        DateOfBirth = DateTime.Now;
    }

    // returns a tuple of boolean and string
    // boolean is true if all the validation methods return true
    // string is empty if all the validation methods return true
    // string contains the error message if any of the validation methods return false
    public (bool, string) IsValid()
    {  
        var validationMethods = new List<Func<(bool, string)>>
        {
            IsValidEmail,
            IsValidUserName,
            IsValidPassword,
            IsValidDateOfBirth,
            IsValidFirstName,
            IsValidLastName,
            IsValidGender,
            IsValidImageFilePath
        };

        foreach (var validationMethod in validationMethods)
        {
            var (isValid, message) = validationMethod();
            if (!isValid)
            {
                return (false, message);
            }
        }
        return (true, string.Empty);
    }

    public (bool, string) IsValidEmail()
    {
        string pattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
        if (string.IsNullOrEmpty(Email))
        {
            return (false, "Email cannot be empty.");
        }
        else if(!Regex.IsMatch(Email, pattern))
        {
            return (false, "Email is not valid.");
        }
        return (true, string.Empty);
    }

    public (bool,string) IsValidUserName()
    {
        if (string.IsNullOrEmpty(Username))
        {
            return (false, "Username cannot be empty.");
        }
        return (true, string.Empty);
    }

    public (bool, string) IsValidPassword()
    {
        if (string.IsNullOrEmpty(Password))
        {
            return (false, "Password cannot be empty.");
        }
        return (true, string.Empty);
    }

    public (bool, string) IsValidDateOfBirth()
    {
        if (DateOfBirth >= DateTime.Now.Date)
        {
            return (false, "Date of birth cannot be today's date.");
        }
        return (true, string.Empty);
    }

    public (bool, string) IsValidFirstName()
    {
        if (string.IsNullOrEmpty(FirstName))
        {
            return (false, "First name cannot be empty.");
        }
        return (true, string.Empty);
    }

    public (bool, string) IsValidLastName()
    {
        if (string.IsNullOrEmpty(LastName))
        {
            return (false, "Last name cannot be empty.");
        }
        return (true, string.Empty);
    }

    public (bool, string) IsValidGender()
    {
        if (string.IsNullOrEmpty(Gender))
        {
            return (false, "Gender cannot be empty.");
        }
        return (true, string.Empty);
    }

    public (bool, string) IsValidImageFilePath()
    {
        if (string.IsNullOrEmpty(ImageFilePath))
        {
            return (false, "Please upload a picture first.");
        }
        return (true, string.Empty);
    }
}
