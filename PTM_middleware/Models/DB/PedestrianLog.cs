using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTM_middleware.DAL;

namespace PTM_middleware.Models.DB
{
    public class PedestrianLog : DataModel
    {
        public string Code { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SignalId { get; set; }
        public string SignalLocation { get; set; }
    }

    public class SignalSwitch : DataModel
    {
        public string Code { get; set; }
        public string State { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SignalId { get; set; }
        public string SignalLocation { get; set; }
    }

    public class PedestrianCrossingLog : DataModel
    {
        public string Code { get; set; }
        public string Trigger { get; set; }
        public int PplCount { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
        public string SignalId { get; set; }
        public string SignalLocation { get; set; }
    }

    public class TrafficDensityLog : DataModel
    {
        public string Code { get; set; }
        public Double Density { get; set; }
        public string Label { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SignalId { get; set; }
        public string SignalLocation { get; set; }
    }
}
