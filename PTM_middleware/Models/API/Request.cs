using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTM_middleware.Models.API
{
    public class PedestrianDetailRequest
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string SignalId { get; set; }
        public string SignalLocation { get; set; }
    }

    public class SignalSwitchRequest
    {
        public string State { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SignalId { get; set; }
        public string SignalLocation { get; set; }
    }

    public class PedestrianCrossingRequest
    {
        public string Trigger { get; set; }
        public int PplCount { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SignalId { get; set; }
        public string SignalLocation { get; set; }
    }

    public class TrafficDensityRequest
    {
        public Double Density { get; set; }
        public string Label { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SignalId { get; set; }
        public string SignalLocation { get; set; }
    }


}
