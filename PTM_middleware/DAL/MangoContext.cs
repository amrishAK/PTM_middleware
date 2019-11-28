using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PTM_middleware.Models.DB;

namespace PTM_middleware.DAL
{
    public class MongoContext
    {
        protected readonly IMongoDatabase _database;
        public string ConnectionString
        {
            get
            {
                return "mongoConnectionString";
            }
        }
        public string DataBase
        {
            get
            {
                return "ptm_db";
            }
        }


        public IMongoCollection<PedestrianLog> PedestrianLogCollection
        {
            get
            {
                return _database.GetCollection<PedestrianLog>("pedestrianLogCollection");
            }
        }

        public IMongoCollection<SignalSwitch> SignalSwitchCollection
        {
            get
            {
                return _database.GetCollection<SignalSwitch>("signalSwitchCollection");
            }
        }
        public IMongoCollection<PedestrianCrossingLog> PedestrianCrossingLogCollection
        {
            get
            {
                return _database.GetCollection<PedestrianCrossingLog>("pedestrianCrossingLogCollection");
            }
        }
        public IMongoCollection<TrafficDensityLog> TrafficDensityLogCollection
        {
            get
            {
                return _database.GetCollection<TrafficDensityLog>("trafficDensityLogCollection");
            }
        }

        public MongoContext()
        {
            try
            {
                var client = new MongoClient(ConnectionString);

                if (client != null)
                {
                    string dataBase = DataBase;
                    _database = client.GetDatabase(dataBase);
                }
            }
            catch
            {
            }
            
        }
    }
}
