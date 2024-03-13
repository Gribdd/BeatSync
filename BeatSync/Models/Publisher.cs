using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.Models;

public partial class Publisher : ObservableObject
{
    [ObservableProperty]
    private int _id;

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
    private int _accounType;

    [ObservableProperty]
    private bool _isDeleted;

    [ObservableProperty]
    private string? _imageFilePath;
}
