namespace BeatSync.Services.Service;

public class ArtistService : GenericService<Artist>
{
    public ArtistService(IUnitofWork unitofWork) : base(unitofWork)
    {
    }

    public override async Task UpdateAsync(int id)
    {
        var artistToBeUpdated = await GetAsync(id);
        string[] editOptions = { "Email", "Username", "Password", "First Name", "Last Name" };
        string selectedOption = await Shell.Current.DisplayActionSheet("Select Property to Edit", "Cancel", null, editOptions);

        var newValue = string.Empty;
        for (int index = 0; index < editOptions.Length; index++)
        {
            if (editOptions[index] == selectedOption)
            {
                switch (index)
                {
                    case 0: // Email
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: artistToBeUpdated.Email);
                        artistToBeUpdated.Email = newValue;
                        break;
                    case 1: // Username
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: artistToBeUpdated.Username);
                        artistToBeUpdated.Username = newValue;
                        break;
                    case 2: // Password
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: artistToBeUpdated.Password);
                        artistToBeUpdated.Password = newValue;
                        break;
                    case 3: // first name
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: artistToBeUpdated.FirstName);
                        artistToBeUpdated.FirstName = newValue;
                        break;
                    case 4: // last name
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Artist {selectedOption}", $"Enter new {selectedOption}:", initialValue: artistToBeUpdated.LastName);
                        artistToBeUpdated.LastName = newValue;
                        break;
                    default:
                        break;
                }
                break;
            }
        }
        await base.UpdateAsync(artistToBeUpdated);
    }
}
