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
}
