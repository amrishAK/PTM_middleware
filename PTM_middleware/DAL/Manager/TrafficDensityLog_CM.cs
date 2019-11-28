using MongoDB.Driver;
using PTM_middleware.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTM_middleware.DAL.Manager
{
    public class TrafficDensityLog_CM : IDataRepository<TrafficDensityLog>
    {
        public string _collectionCodePrefix
        {
            get
            {
                return "PDL";
            }
        }

        private readonly MongoContext _context;

        public TrafficDensityLog_CM(MongoContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(TrafficDensityLog document)
        {
            try
            {
                Random _rdm = new Random();
                int id2 = _rdm.Next(000000, 1000000);
                int id1 = Convert.ToInt32(DateTime.Now.ToString("mmssff"));
                var id = id1 | id2;

                document.Code = _collectionCodePrefix + id.ToString();
                await _context.TrafficDensityLogCollection.InsertOneAsync(document);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public async Task<List<TrafficDensityLog>> GetAllAsync(string getDocs = "active")
        {
            try
            {
                return await _context.TrafficDensityLogCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<TrafficDensityLog> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TrafficDensityLog document)
        {
            throw new NotImplementedException();
        }
    }
}
