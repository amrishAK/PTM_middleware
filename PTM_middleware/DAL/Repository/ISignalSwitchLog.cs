using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTM_middleware.Models.DB;

namespace PTM_middleware.DAL.Repository
{
    public interface ISignalSwitchLog : IDataRepository<SignalSwitch>
    {
        string _collectionCodePrefix { get; }
    }
}
