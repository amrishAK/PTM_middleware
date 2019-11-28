using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IERP.Helper;
using PTM_middleware.DAL.Repository;
using PTM_middleware.Models.DB;
using PTM_middleware.Models.API;
using PTM_middleware.DAL;

namespace PTM_middleware.BLL
{
    public class Traffic_BL 
    {
        private readonly IPedestrianLog _pedestrianLog;
        private readonly ISignalSwitchLog _signalSwitchLog;
        private readonly IDataRepository<PedestrianCrossingLog> _pedestrianRepository;
        private readonly IDataRepository<TrafficDensityLog> _trafficDensityRepository;

        public Traffic_BL(IPedestrianLog pedestrianLog, ISignalSwitchLog signalSwitchLog,IDataRepository<PedestrianCrossingLog> pedestrianRepository, IDataRepository<TrafficDensityLog> trafficDensityRepository)
        {
            _pedestrianLog = pedestrianLog;
            _signalSwitchLog = signalSwitchLog;
            _trafficDensityRepository = trafficDensityRepository;
            _pedestrianRepository = pedestrianRepository;
        }

        public async Task<bool> CreateAsync(PedestrianDetailRequest request)
        {
            var copier = new ClassValueCopier();
            PedestrianLog newChildPart = copier.ConvertAndCopy<PedestrianLog, PedestrianDetailRequest>(request);
            return await _pedestrianLog.AddAsync(newChildPart);
        }

        public async Task<bool> CreateSignalSwitchLog(SignalSwitchRequest request)
        {
            var copier = new ClassValueCopier();
            SignalSwitch newChildPart = copier.ConvertAndCopy<SignalSwitch, SignalSwitchRequest>(request);
            return await _signalSwitchLog.AddAsync(newChildPart);
        }

        public async Task<bool> CreatePedestrianCrossingLog(PedestrianCrossingRequest request)
        {
            var copier = new ClassValueCopier();
            PedestrianCrossingLog newChildPart = copier.ConvertAndCopy<PedestrianCrossingLog, PedestrianCrossingRequest>(request);
            return await _pedestrianRepository.AddAsync(newChildPart);
        }

        public async Task<bool> CreateTrafficDensityLog(TrafficDensityRequest request)
        {
            var copier = new ClassValueCopier();
            TrafficDensityLog newChildPart = copier.ConvertAndCopy<TrafficDensityLog, TrafficDensityRequest>(request);
            return await _trafficDensityRepository.AddAsync(newChildPart);
        }

        public Task<bool> DeleteDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetAllDocuments(object request = null)
        {
            string type = (string)request;
            switch(type)
            {
                case "PL":
                    var list = await  _pedestrianLog.GetAllAsync();
                    return list;
                case "SS":
                    var ssList = _signalSwitchLog.GetAllAsync();
                    return ssList;
                case "PCL":
                    var dataPcl = await _pedestrianRepository.GetAllAsync();
                    var dataPclList = new List<GetTemplateTD>();
                    dataPcl.ForEach(td =>
                    {
                        dataPclList.Add(new GetTemplateTD
                        {
                            DateTimeStamp = $"{td.Date} {td.Time}",
                            Data = td.PplCount,
                        });
                    });
                    return dataPclList;
                case "TD":
                    var data = await _trafficDensityRepository.GetAllAsync();
                    var dataList = new List<GetTemplateTD>();
                    data.ForEach(td =>
                    {
                        dataList.Add(new GetTemplateTD
                        {
                            DateTimeStamp = $"{td.Date} {td.Time}",
                            Data = td.Density,
                        });
                    });
                    return dataList;

                default:
                    throw new Exception();
            }
        }

        public Task<object> GetDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }
    }
}
