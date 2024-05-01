using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.Services.IService;

public interface IHistoryService : IGenericService<History>
{
    Task<ObservableCollection<History>> GetHistoriesByUserIdAsync(int userId, int accountType);
}
