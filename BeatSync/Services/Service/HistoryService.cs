using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.Services.Service;

public class HistoryService : GenericService<History>, IHistoryService
{
    public HistoryService(IUnitofWork unitofWork) : base(unitofWork)
    {

    }

    public async Task<ObservableCollection<History>> GetHistoriesByUserIdAsync(int userId, int accountType)
    {
        var histories = await GetAllAsync();
        return new ObservableCollection<History>(histories
            .Where(h => h.UserId == userId && h.AccountType == accountType)
            .OrderBy(h => h.TimeStamp));
    }
}
