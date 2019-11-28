using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTM_middleware.DAL;

namespace PTM_middleware.Models.DB
{
    public class OpenData
    {
        public int OBJECTID { get; set; }
        public int RoadId { get; set; }
        public string SegmentId { get; set; }
        public string RoadClass { get; set; }
        public float Length { get; set; }
        public string RoadType { get; set; }
        public string FootpathTy { get; set; }
        public string AgeInYears { get; set; }
        public string EngineersA { get; set; }
        public string StreetDesc { get; set; }


    }
}
