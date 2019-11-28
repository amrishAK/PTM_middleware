using PTM_middleware.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTM_middleware.DAL.Repository
{
    public interface IPedestrianLog : IDataRepository<PedestrianLog>
    {
        string _collectionCodePrefix { get; }
    }
}
